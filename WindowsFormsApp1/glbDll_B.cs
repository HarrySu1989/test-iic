using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
  internal class glbDll_B
  {
    /// <summary>
    /// get IntPtr about all of Devs
    /// </summary>
    /// <param name="HIDGuid"></param>
    /// <returns></returns>
    internal static IntPtr GetClassDevOfHandle(Guid HIDGuid)
    {
      return glbDll_A.SetupDiGetClassDevs(ref HIDGuid, 0, IntPtr.Zero, glbCls.DIGCF.DIGCF_PRESENT | glbCls.DIGCF.DIGCF_DEVICEINTERFACE);
    }

    /// <summary>
    /// get interfaces information
    /// </summary>
    /// <param name="HIDInfoSet"></param>
    /// <param name="HIDGuid"></param>
    /// <param name="index"></param>
    /// <param name="interfaceInfo"></param>
    /// <returns></returns>
    internal static bool GetEnumDeviceInterfaces(IntPtr HIDInfoSet, ref Guid HIDGuid, uint index, ref glbCls.SP_DEVICE_INTERFACE_DATA interfaceInfo)
    {
      return glbDll_A.SetupDiEnumDeviceInterfaces(HIDInfoSet, IntPtr.Zero, ref HIDGuid, index, ref interfaceInfo);
    }

    /// <summary>
    /// get interface detail information
    /// </summary>
    /// <param name="HIDInfoSet"></param>
    /// <param name="interfaceInfo"></param>
    /// <param name="buffsize"></param>
    /// <param name="buffsize_2"></param>
    internal static bool GetDeviceInterfaceDetail(IntPtr HIDInfoSet, ref glbCls.SP_DEVICE_INTERFACE_DATA interfaceInfo, IntPtr pDetail, ref int buffsize)
    {
      return glbDll_A.SetupDiGetDeviceInterfaceDetail(HIDInfoSet, ref interfaceInfo, pDetail, buffsize, ref buffsize, null);
    }

    /// <summary>
    /// get device guid
    /// </summary>
    /// <param name="HIDGuid"></param>
    internal static void GetDeviceGuid(ref Guid HIDGuid)
    {
      glbDll_A.HidD_GetHidGuid(ref HIDGuid);
    }

    internal static bool GetIndexedString(IntPtr deviceInfoSet, UInt32 IndexSerial, IntPtr pdeviceString, UInt32 BufferLength)
    {
      return glbDll_A.HidD_GetIndexedString(deviceInfoSet, IndexSerial, pdeviceString, BufferLength);
    }

    internal static bool CloseDeviceFile(IntPtr intHandle)
    {
      return glbDll_A.CloseHandle(intHandle);
    }

    /// <summary>
    /// Destroy Device information list
    /// </summary>
    /// <param name="HIDInfoSet"></param>
    internal static void DestroyDeviceInfoList(IntPtr HIDInfoSet)
    {
      glbDll_A.SetupDiDestroyDeviceInfoList(HIDInfoSet);
    }
  }
}