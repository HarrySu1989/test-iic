using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static WindowsFormsApp1.test;

namespace WindowsFormsApp1
{
  internal class glbDll_A
  {
    /// <summary>
    /// The SetupDiDestroyDeviceInfoList function deletes a device information set and frees all associated memory.
    /// </summary>
    /// <param name="DeviceInfoSet">A handle to the device information set to delete.</param>
    /// <returns>returns TRUE if it is successful. Otherwise, it returns FALSE </returns>
    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern Boolean SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

    /// <summary>
    /// The HidD_GetHidGuid routine returns the device interface GUID for HIDClass devices.
    /// </summary>
    /// <param name="HidGuid">a caller-allocated GUID buffer that the routine uses to return the device interface GUID for HIDClass devices.</param>
    [DllImport("hid.dll")]
    public static extern void HidD_GetHidGuid(ref Guid HidGuid);

    /// <summary>
    /// The SetupDiGetClassDevs function returns a handle to a device information set that contains requested device information elements for a local machine.
    /// </summary>
    /// <param name="ClassGuid">GUID for a device setup class or a device interface class. </param>
    /// <param name="Enumerator">A pointer to a NULL-terminated string that supplies the name of a PnP enumerator or a PnP device instance identifier. </param>
    /// <param name="HwndParent">A handle of the top-level window to be used for a user interface</param>
    /// <param name="Flags">A variable  that specifies control options that filter the device information elements that are added to the device information set.
    /// </param>
    /// <returns>a handle to a device information set </returns>
    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, uint Enumerator, IntPtr HwndParent, glbCls.DIGCF Flags);

    /// <summary>
    /// The SetupDiEnumDeviceInterfaces function enumerates the device interfaces that are contained in a device information set.
    /// </summary>
    /// <param name="deviceInfoSet">A pointer to a device information set that contains the device interfaces for which to return information</param>
    /// <param name="deviceInfoData">A pointer to an SP_DEVINFO_DATA structure that specifies a device information element in DeviceInfoSet</param>
    /// <param name="interfaceClassGuid">a GUID that specifies the device interface class for the requested interface</param>
    /// <param name="memberIndex">A zero-based index into the list of interfaces in the device information set</param>
    /// <param name="deviceInterfaceData">a caller-allocated buffer that contains a completed SP_DEVICE_INTERFACE_DATA structure that identifies an interface that meets the search parameters</param>
    /// <returns></returns>
    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr deviceInfoSet, IntPtr deviceInfoData, ref Guid interfaceClassGuid, UInt32 memberIndex, ref glbCls.SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

    /// <summary>
    /// The SetupDiGetDeviceInterfaceDetail function returns details about a device interface.
    /// </summary>
    /// <param name="deviceInfoSet">A pointer to the device information set that contains the interface for which to retrieve details</param>
    /// <param name="deviceInterfaceData">A pointer to an SP_DEVICE_INTERFACE_DATA structure that specifies the interface in DeviceInfoSet for which to retrieve details</param>
    /// <param name="deviceInterfaceDetailData">A pointer to an SP_DEVICE_INTERFACE_DETAIL_DATA structure to receive information about the specified interface</param>
    /// <param name="deviceInterfaceDetailDataSize">The size of the DeviceInterfaceDetailData buffer</param>
    /// <param name="requiredSize">A pointer to a variable that receives the required size of the DeviceInterfaceDetailData buffer</param>
    /// <param name="deviceInfoData">A pointer buffer to receive information about the device that supports the requested interface</param>
    /// <returns></returns>
    [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref glbCls.SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData, int deviceInterfaceDetailDataSize, ref int requiredSize, glbCls.SP_DEVINFO_DATA deviceInfoData);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern Boolean CloseHandle(IntPtr hObject);

    [DllImport("hid.dll")]
    public static extern bool HidD_GetIndexedString(IntPtr deviceInfoSet, UInt32 IndexSerial, IntPtr pdeviceString, UInt32 BufferLength);
  }
}