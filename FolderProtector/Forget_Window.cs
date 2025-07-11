﻿using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderProtector
{
    public partial class Forget_Window : AntdUI.Window
    {
        //函数========================================================================================
        #region Functions

        #region DLL引用
        //DLL引用==================================================
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);
        //
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SendMessageTimeout(
        IntPtr hWnd,
        uint Msg,
        UIntPtr wParam,
        string lParam,
        uint fuFlags,
        uint uTimeout,
        out UIntPtr lpdwResult);
        private const uint WM_SETTINGCHANGE = 0x001A;
        private const uint SMTO_ABORTIFHUNG = 0x0002;
        //
        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        private class ShellLink { }
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        private interface IShellLinkW
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxArgs);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("0000010b-0000-0000-C000-000000000046")]
        private interface IPersistFile
        {
            void GetClassID(out Guid pClassID);
            [PreserveSig]
            int IsDirty();
            void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);
            void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);
            void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
            void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
        }
        //DLL引用end==================================================
        #endregion

        //写日志
        public static void Log(string level, string message)
        {
            // 获取当前时间并格式化
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            // 构造完整日志条目
            string logContent = $"[{timestamp}][{level}]: {message}";

            // 拼接完整文件路径
            string logPath = Path.Combine(Application.StartupPath, "Log.log");

            // 使用追加模式写入文件
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine(logContent);
            }

        }
        //连通性测试
        public static object CheckUrlConnection(string url)
        {
            // 验证URL格式有效性
            try
            {
                var uri = new Uri(url);
            }
            catch (UriFormatException)
            {
                return "unconnect";
            }

            HttpWebRequest request = null;
            Stopwatch sw = new Stopwatch();

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;     // 设置5秒超时
                request.Method = "HEAD";     // 使用HEAD方法减少数据量

                sw.Start();
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    sw.Stop();
                    return sw.ElapsedMilliseconds;
                }
            }
            catch (WebException ex)
            {
                sw.Stop();
                /* 服务器响应但返回错误状态（如404）的情况
                   仍视为连接成功，返回延迟时间 */
                if (ex.Response != null)
                {
                    return sw.ElapsedMilliseconds;
                }
                return "unconnect"; // 真正无法连接的情况
            }
            catch (Exception)
            {
                return "unconnect";
            }
            finally
            {
                request?.Abort(); // 确保释放网络资源
            }
        }

        //执行控制台命令
        public string ExecuteCommand(string command)
        {
            try
            {
                var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                {
                    CreateNoWindow = false,          // 不创建新窗口
                    UseShellExecute = false,        // 不使用系统外壳程序执行
                    RedirectStandardError = true,   // 重定向标准错误
                    RedirectStandardOutput = true   // 重定向标准输出
                };

                using (var process = new Process())
                {
                    process.StartInfo = processInfo;
                    process.Start();

                    // 异步读取输出流和错误流
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();  // 等待程序执行完成

                    // 组合输出结果
                    string result = string.IsNullOrEmpty(output) ? "" : output;
                    string errorResult = string.IsNullOrEmpty(error) ? "" : "\n[Error]\n" + error;

                    return $"{result}{errorResult} (ExitCode: {process.ExitCode})";
                }
            }
            catch (Exception ex)
            {
                return $"执行命令时发生异常：{ex.Message}";
            }
        }

        //弹出系统通知
        public static void ShowNotification(string title, string content)
        {
            NotifyIcon notifyIcon = new NotifyIcon();

            // 创建透明图标
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                bmp.SetPixel(0, 0, Color.Transparent);
                IntPtr hIcon = bmp.GetHicon();
                try
                {
                    notifyIcon.Icon = Icon.FromHandle(hIcon);
                }
                finally
                {
                    DestroyIcon(hIcon);
                }
            }

            notifyIcon.Visible = true;

            // 设置通知关闭后的清理操作
            notifyIcon.BalloonTipClosed += (sender, e) =>
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            };

            // 显示通知（3000ms=3秒显示时间）
            notifyIcon.ShowBalloonTip(3000, title, content, ToolTipIcon.None);
        }

        //写注册表项
        //rootKey常用常量
        //Registry.CurrentUser (HKEY_CURRENT_USER)
        //Registry.LocalMachine (HKEY_LOCAL_MACHINE)
        //Registry.ClassesRoot (HKEY_CLASSES_ROOT)

        /*valueKind：支持的类型包括：
        String：字符串值
        DWord：32位整数
        QWord：64位整数
        Binary：二进制数据
        MultiString：字符串数组*/
        public static bool WriteRegistryValue(RegistryKey rootKey, string subKeyPath, string valueName, object value, RegistryValueKind valueKind)
        {
            try
            {
                if (rootKey == null)
                    throw new ArgumentNullException(nameof(rootKey));

                if (string.IsNullOrEmpty(subKeyPath))
                    throw new ArgumentException("子项路径不能为空", nameof(subKeyPath));

                using (RegistryKey subKey = rootKey.CreateSubKey(subKeyPath))
                {
                    if (subKey == null) return false;

                    subKey.SetValue(valueName, value, valueKind);
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 权限不足，可能需要以管理员身份运行
                throw;
            }
            catch (Exception ex)
            {
                // 记录异常或处理其他错误
                Console.WriteLine($"写入注册表失败: {ex.Message}");
                return false;
            }
        }

        //读注册表项
        public static object ReadRegistryValue(RegistryKey rootKey, string subKeyPath, string valueName, object defaultValue = null)
        {
            try
            {
                if (rootKey == null)
                    throw new ArgumentNullException(nameof(rootKey));

                if (string.IsNullOrEmpty(subKeyPath))
                    throw new ArgumentException("子项路径不能为空", nameof(subKeyPath));

                using (RegistryKey subKey = rootKey.OpenSubKey(subKeyPath, false))
                {
                    // 子项不存在时返回默认值
                    if (subKey == null) return defaultValue;

                    // 获取值（值不存在时返回默认值）
                    return subKey.GetValue(valueName, defaultValue);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 权限不足，可能需要管理员权限
                throw;
            }
            catch (Exception ex)
            {
                // 记录异常或处理其他错误
                Console.WriteLine($"读取注册表失败: {ex.Message}");
                return defaultValue;
            }
        }

        // 写入配置
        public static void WriteConfig(string filePath, string section, string key, string value)
        {
            var sections = ParseConfigFile(filePath);

            // 创建或更新节
            if (!sections.ContainsKey(section))
            {
                sections[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            // 更新键值
            sections[section][key.Trim()] = value;

            // 生成配置文件内容
            var lines = new List<string>();

            // 处理默认节（空节名）
            if (sections.TryGetValue("", out var defaultSection) && defaultSection.Count > 0)
            {
                lines.AddRange(defaultSection.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            // 处理带节名的配置（按字母顺序排序）
            foreach (var sec in sections.Keys
                .Where(s => !string.IsNullOrEmpty(s))
                .OrderBy(s => s, StringComparer.OrdinalIgnoreCase))
            {
                // 添加节分隔空行
                if (lines.Count > 0) lines.Add("");

                lines.Add($"[{sec}]");
                lines.AddRange(sections[sec].Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        // 读取配置
        public static string ReadConfig(string filePath, string section, string key)
        {
            if (!File.Exists(filePath)) return null;

            var sections = ParseConfigFile(filePath);

            if (sections.TryGetValue(section, out var sectionData) &&
                sectionData.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }

        // 解析配置文件为节字典(读写配置)
        private static Dictionary<string, Dictionary<string, string>> ParseConfigFile(string filePath)
        {
            var sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            string currentSection = "";

            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var trimmed = line.Trim();

                    // 处理节头
                    if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                    {
                        currentSection = trimmed.Substring(1, trimmed.Length - 2).Trim();
                        if (!sections.ContainsKey(currentSection))
                        {
                            sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }
                        continue;
                    }

                    // 处理键值对
                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]))
                    {
                        var k = parts[0].Trim();
                        var v = parts[1].Trim();

                        if (!sections.ContainsKey(currentSection))
                        {
                            sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }

                        sections[currentSection][k] = v;
                    }
                }
            }
            return sections;
        }

        //删除配置
        public static void DeleteConfig(string filePath, string section, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty", nameof(key));

            var sections = ParseConfigFile(filePath);
            section = section ?? "";

            if (sections.TryGetValue(section, out var sectionData) && sectionData.Remove(key))
            {
                // 如果节已空则移除整个节
                if (sectionData.Count == 0)
                {
                    sections.Remove(section);
                }

                SaveSectionsToFile(filePath, sections);

            }
        }

        // 统一保存配置的方法
        private static void SaveSectionsToFile(string filePath, Dictionary<string, Dictionary<string, string>> sections)
        {
            var lines = new List<string>();

            // 处理默认节
            if (sections.TryGetValue("", out var defaultSection) && defaultSection.Count > 0)
            {
                lines.AddRange(defaultSection.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            // 处理带节名的配置（按字母排序）
            foreach (var sec in sections.Keys
                .Where(s => !string.IsNullOrEmpty(s))
                .OrderBy(s => s, StringComparer.OrdinalIgnoreCase))
            {
                var sectionData = sections[sec];
                if (sectionData.Count == 0) continue;

                if (lines.Count > 0) lines.Add("");
                lines.Add($"[{sec}]");
                lines.AddRange(sectionData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }

            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }

        //删除配置节
        public static void DeleteSection(string filePath, string section)
        {
            var sections = ParseConfigFile(filePath);
            section = section ?? "";

            if (sections.Remove(section))
            {
                SaveSectionsToFile(filePath, sections);
            }
        }

        //写PATH系统环境变量
        public static void AddPath(string directoryPath, bool systemLevel = true)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("目录路径不能为空");

            RegistryKey registryKey = systemLevel ?
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", true) :
                Registry.CurrentUser.OpenSubKey(@"Environment", true);

            if (registryKey == null)
                throw new NullReferenceException("注册表项未找到");

            try
            {
                string currentPath = registryKey.GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames).ToString();
                string[] paths = currentPath.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // 检查是否已存在（不区分大小写）
                if (paths.Any(p => p.Trim().Equals(directoryPath.Trim(), StringComparison.OrdinalIgnoreCase)))
                    return;

                // 追加新路径
                string newPath = currentPath.TrimEnd(';') + ";" + directoryPath.Trim();

                // 更新注册表
                registryKey.SetValue("PATH", newPath, RegistryValueKind.ExpandString);

                // 广播环境变量变更通知
                SendMessageTimeout(
                    new IntPtr(0xFFFF), // HWND_BROADCAST
                    WM_SETTINGCHANGE,
                    UIntPtr.Zero,
                    "Environment",
                    SMTO_ABORTIFHUNG,
                    5000,
                    out UIntPtr _);
            }
            finally
            {
                registryKey.Close();
            }
        }

        //创建快捷方式
        public static bool CreateShortcut(string targetPath, string shortcutPath)
        {
            if (!File.Exists(targetPath)) return false;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(shortcutPath));

                var shellLink = (IShellLinkW)new ShellLink();
                shellLink.SetPath(targetPath);
                shellLink.SetWorkingDirectory(Path.GetDirectoryName(targetPath));
                shellLink.SetIconLocation(targetPath, 0);  // 使用目标文件自身图标

                var persistFile = (IPersistFile)shellLink;
                persistFile.Save(shortcutPath, false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //程序自启动
        public static bool SetAutoStart(bool enable, string exePath = null, RegistryKey registryRoot = null, string keyName = null)
        {
            try
            {
                // 设置默认值
                exePath = exePath ?? Application.ExecutablePath;
                registryRoot = registryRoot ?? Registry.CurrentUser;
                keyName = keyName ?? Application.ProductName;

                // 获取注册表Run子项
                using (var runKey = registryRoot.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Run",
                    true)) // 需要写权限
                {
                    if (runKey == null)
                    {
                        throw new Exception("无法打开注册表Run项");
                    }

                    if (enable)
                    {
                        // 设置自启动
                        runKey.SetValue(keyName, exePath);
                    }
                    else
                    {
                        // 移除自启动
                        runKey.DeleteValue(keyName, throwOnMissingValue: false);
                    }
                }
                return true;
            }
            catch (SecurityException ex)
            {
                MessageBox.Show($"需要管理员权限才能修改系统级自启动设置\n{ex.Message}");
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"访问被拒绝，请以管理员身份运行\n{ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查当前自启动状态
        /// </summary>
        public static bool IsAutoStartEnabled(RegistryKey registryRoot = null,
                                            string keyName = null)
        {
            try
            {
                registryRoot = registryRoot ?? Registry.CurrentUser;
                keyName = keyName ?? Application.ProductName;

                using (var runKey = registryRoot.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Run"))
                {
                    var value = runKey?.GetValue(keyName);
                    return value != null && value.ToString().Equals(
                        Application.ExecutablePath,
                        StringComparison.OrdinalIgnoreCase);
                }
            }
            catch
            {
                return false;
            }
        }

        //移动文件夹
        public bool MoveFolder(string source, string dest, bool overwrite, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                // 检查源文件夹
                if (!Directory.Exists(source))
                {
                    errorMessage = "源文件夹不存在";
                    return false;
                }

                // 判断是否跨磁盘移动（关键逻辑）
                bool isSameDrive = Path.GetPathRoot(source)?.ToUpper()
                                == Path.GetPathRoot(dest)?.ToUpper();

                // 处理目标文件夹已存在的情况
                if (Directory.Exists(dest))
                {
                    if (overwrite) Directory.Delete(dest, true);
                    else
                    {
                        errorMessage = "目标文件夹已存在且未启用覆盖";
                        return false;
                    }
                }

                // 执行移动
                if (isSameDrive)
                {
                    Directory.Move(source, dest); // 同一磁盘直接移动
                }
                else
                {
                    // 跨磁盘：复制+删除方案
                    new Computer().FileSystem.CopyDirectory(source, dest, overwrite);
                    Directory.Delete(source, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"{(ex is IOException ? "IO错误" : ex.GetType().Name)}: {ex.Message}";
                return false;
            }
        }

        //复制文件夹
        public bool CopyFolder(string sourceFolder, string destFolder, bool overwrite)
        {
            try
            {
                // 检查源文件夹是否存在
                if (!Directory.Exists(sourceFolder))
                {
                    return false;
                }

                // 处理目标文件夹已存在的情况
                if (Directory.Exists(destFolder))
                {
                    if (overwrite)
                    {
                        // 递归删除目标文件夹
                        Directory.Delete(destFolder, true);
                    }
                    else
                    {
                        return false;
                    }
                }

                // 创建目标目录结构
                Directory.CreateDirectory(Path.GetDirectoryName(destFolder));

                // 执行复制操作（自动处理所有子内容和文件）
                new Computer().FileSystem.CopyDirectory(
                    sourceFolder,
                    destFolder,
                    overwrite
                );

                return true;
            }
            catch
            {
                return false;
            }
        }

        //异步HTTP下载文件
        public async Task DownloadFileAsync(string url, string savePath, IProgress<(int ProgressPercentage, long BytesReceived)> progress = null, CancellationToken cancellationToken = default)
        {
            using (var httpClient = new HttpClient())
            {
                // 获取响应头并验证状态
                using (var response = await httpClient.GetAsync(
                    url,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    // 创建保存目录
                    var directory = Path.GetDirectoryName(savePath);
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                    // 获取文件总大小
                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    var receivedBytes = 0L;

                    // 创建文件流
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(
                        savePath,
                        FileMode.Create,
                        FileAccess.Write,
                        FileShare.None,
                        bufferSize: 8192,
                        useAsync: true))
                    {
                        var buffer = new byte[8192];
                        int bytesRead;

                        // 分段下载并更新进度
                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                            receivedBytes += bytesRead;

                            if (totalBytes > 0)
                            {
                                var progressPercentage = (int)((double)receivedBytes / totalBytes * 100);
                                progress?.Report((progressPercentage, receivedBytes));
                            }
                            else
                            {
                                progress?.Report((-1, receivedBytes));
                            }
                        }
                    }
                }
            }
        }

        //异步HTTP读文件
        public async Task<string> HttpReadFileAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 设置超时时间（可选）
                    client.Timeout = TimeSpan.FromSeconds(30);

                    // 发送GET请求
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();  // 确保响应成功
                        return await response.Content.ReadAsStringAsync();  // 读取内容为字符串
                    }
                }
            }
            catch (Exception ex)
            {
                // 这里可以记录异常或进行其他处理
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;  // 或者根据需求返回空字符串/抛出异常
            }
        }

        //异步HTTP下载文件带进度
        public async Task DownloadFileAsync(string url, string savePath, IProgress<int> progress)
        {
            using (WebClient webClient = new WebClient())
            {
                // 设置进度报告事件
                webClient.DownloadProgressChanged += (sender, e) =>
                {
                    progress?.Report(e.ProgressPercentage);
                };

                // 异步下载文件
                await webClient.DownloadFileTaskAsync(new Uri(url), savePath);
            }
        }
        #endregion

        //字符串转哈希
        public static string StringToSHA256(string input)
        {
            // 检查输入是否为null或空
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                // 将字符串转换为字节数组
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                // 计算哈希值
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // 将字节数组转换为十六进制字符串
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // "x2" 表示两位小写十六进制
                }

                return builder.ToString();
            }
        }

        //变量========================================================================================
        string[] chars = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        //事件========================================================================================
        public Forget_Window()
        {
            InitializeComponent();
            AntdUI.Config.TextRenderingHighQuality = true;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Done_Click(object sender, EventArgs e)
        {
            if (button_Done.Text == "下一步")
            {
                if (input.Text == Convert.ToString(ReadRegistryValue(Registry.CurrentUser, "SOFTWARE\\zProtectFolder", "RecoveryCode"))) 
                {
                    button_Done.Text = "完成";
                    label1.Text = "请输入您的新密钥";
                    input.Text = "";
                }
                else
                {
                    AntdUI.Modal.open(new AntdUI.Modal.Config(this, "", "")
                    {
                        Title = "恢复码错误！",
                        Content = "请输入首次设置密钥时在C:\\生成的恢复码",
                        CancelText = null,
                        OkText = "重新尝试",
                        Icon = AntdUI.TType.Error
                    });
                }
            }
            else if (button_Done.Text == "完成")
            {
                if (StringToSHA256(input.Text) == Convert.ToString(ReadRegistryValue(Registry.CurrentUser, "SOFTWARE\\zProtectFolder", "Password")))
                {
                    AntdUI.Modal.open(new AntdUI.Modal.Config(this, "", "")
                    {
                        Title = "设置失败",
                        Content = "您的新密钥不可与旧密钥相同！",
                        CancelText = null,
                        OkText = "重新尝试",
                        Icon = AntdUI.TType.Error
                    });
                }
                else
                {
                    WriteRegistryValue(Registry.CurrentUser, "SOFTWARE\\zProtectFolder", "Password", StringToSHA256(input.Text), RegistryValueKind.String);



                    string temp = "";
                    string temp2 = "";
                    Random random = new Random();

                    for (int j = 0; j < 10; j++)
                    {
                        temp = "";
                        for (int i = 0; i < 10; i++)
                        {
                            temp = temp + chars[random.Next(0, 61)];
                        }
                        temp2 = temp2 + temp + "-";
                    }

                    File.WriteAllText("C:\\FolderProtector_Recoverycode.txt", temp2.Substring(0, temp2.Length - 1));

                    WriteRegistryValue(Registry.CurrentUser, "SOFTWARE\\zProtectFolder", "RecoveryCode", temp2.Substring(0, temp2.Length - 1), RegistryValueKind.String);

                    AntdUI.Modal.open(new AntdUI.Modal.Config(this, "", "")
                    {
                        Title = "密钥设置成功！",
                        Content = "为了您的安全，我们再次在\"C:\\FolderProtector_Recoverycode.txt\"生成了新的密钥恢复码",
                        CancelText = null,
                        OkText = "确定",
                        Icon = AntdUI.TType.Success
                    });

                    this.Close();
                }
            }
        }
    }
}
