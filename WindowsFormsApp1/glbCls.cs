using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsFormsApp1
{
  internal class glbCls
  {
    /// <summary>
    /// SP_DEVICE_INTERFACE_DETAIL_DATA structure contains the path for a device interface.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]// 移除 Pack = 2
    internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
    {
      internal uint cbSize;
      internal short devicePath;
    }

    /// <summary>
    /// SP_DEVINFO_DATA structure defines a device instance that is a member of a device information set.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SP_DEVINFO_DATA
    {
      public uint cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
      public Guid classGuid = Guid.Empty; // temp
      public uint devInst = 0; // dumy
      public IntPtr reserved = IntPtr.Zero;
    }

    /// <summary>
    /// SP_DEVICE_INTERFACE_DATA structure defines a device interface in a device information set.
    /// </summary>
    public struct SP_DEVICE_INTERFACE_DATA
    {
      public uint cbSize;
      public Guid interfaceClassGuid;
      public uint flags;
      public IntPtr reserved;
    }

    /// <summary>
    /// Flags controlling what is included in the device information set built by SetupDiGetClassDevs
    /// </summary>
    public enum DIGCF
    {
      DIGCF_DEFAULT = 0x00000001, // only valid with DIGCF_DEVICEINTERFACE
      DIGCF_PRESENT = 0x00000002,
      DIGCF_ALLCLASSES = 0x00000004,
      DIGCF_PROFILE = 0x00000008,
      DIGCF_DEVICEINTERFACE = 0x00000010
    }

    /// <summary>
    /// HID STATUS
    /// </summary>
    public enum HID_STATUS
    {
      SUCCESS,
      NO_DEVICE,
      NO_FIND,
      OPEND,
      WRITE_FAID,
      READ_FAID
    }

    /// <summary>
    /// This function creates, opens, or truncates a file, COM port, device, service, or console.
    /// </summary>
    /// <param name="fileName">a null-terminated string that specifies the name of the object</param>
    /// <param name="desiredAccess">Type of access to the object</param>
    /// <param name="shareMode">Share mode for object</param>
    /// <param name="securityAttributes">Ignored; set to NULL</param>
    /// <param name="creationDisposition">Action to take on files that exist, and which action to take when files do not exist</param>
    /// <param name="flagsAndAttributes">File attributes and flags for the file</param>
    /// <param name="templateFile">Ignored</param>
    /// <returns>An open handle to the specified file indicates success</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateFile(
   string FileName,                // 文件名
   uint DesiredAccess,             // 访问模式
   uint ShareMode,                 // 共享模式
   uint SecurityAttributes,        // 安全属性
   uint CreationDisposition,       // 如何创建
   uint FlagsAndAttributes,        // 文件属性
   int hTemplateFile               // 模板文件的句柄
   );

    internal struct Struct_Device_List
    {
      public string[] deviceList;
      public string[] deviceSerialNameList;
      public int intCount;
    }

    /// <summary>
    /// Action to take on files that exist, and which action to take when files do not exist.
    /// </summary>
    public static class CREATIONDISPOSITION
    {
      public const uint CREATE_NEW = 1;
      public const uint CREATE_ALWAYS = 2;
      public const uint OPEN_EXISTING = 3;
      public const uint OPEN_ALWAYS = 4;
      public const uint TRUNCATE_EXISTING = 5;
    }

    /// <summary>
    /// File attributes and flags for the file.
    /// </summary>
    public static class FLAGSANDATTRIBUTES
    {
      public const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
      public const uint FILE_FLAG_OVERLAPPED = 0x40000000;
      public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
      public const uint FILE_FLAG_RANDOM_ACCESS = 0x10000000;
      public const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;
      public const uint FILE_FLAG_DELETE_ON_CLOSE = 0x04000000;
      public const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
      public const uint FILE_FLAG_POSIX_SEMANTICS = 0x01000000;
      public const uint FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;
      public const uint FILE_FLAG_OPEN_NO_RECALL = 0x00100000;
      public const uint FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000;
    }

    /// <summary>
    /// Type of access to the object.
    ///</summary>
    public static class DESIREDACCESS
    {
      public const uint FILE_SHARE_READ = 0x00000001;
      public const uint FILE_SHARE_WRITE = 0x00000002;
      public const uint GENERIC_READ = 0x80000000;
      public const uint GENERIC_WRITE = 0x40000000;
      public const uint GENERIC_EXECUTE = 0x20000000;
      public const uint GENERIC_ALL = 0x10000000;
    }
  }
}