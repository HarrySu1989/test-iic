using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsFormsApp1
{
  internal class test
  {
    public static glbCls.Struct_Device_List device_list = new glbCls.Struct_Device_List();

    internal static int get_count()
    {
      device_list.deviceSerialNameList = new string[64];
      device_list.deviceList = new string[64];
      device_list.intCount = 0;
      List<string> listStr = new List<string>();
      int intNUmber = GetNumUsbPort(ref listStr);
      return intNUmber;
    }

    private const int MAX_USB_DEVICES = 64;

    private static int pID_ = 0x6852;

    public static int PID
    {
      set { pID_ = value; }
      get { return pID_; }
    }

    private static int vID_ = 0x8483;

    public static int VID
    {
      set { vID_ = value; }
      get { return vID_; }
    }

    internal static IntPtr CreateDeviceFile(string device)
    {
      //解决IIC通信识别测试板异常问题
      //return CreateFile(device, DESIREDACCESS.GENERIC_READ | DESIREDACCESS.GENERIC_WRITE,0, 0, CREATIONDISPOSITION.OPEN_EXISTING, FLAGSANDATTRIBUTES.FILE_FLAG_OVERLAPPED, 0);
      return glbCls.CreateFile(device, glbCls.DESIREDACCESS.GENERIC_READ | glbCls.DESIREDACCESS.GENERIC_WRITE, glbCls.DESIREDACCESS.FILE_SHARE_READ | glbCls.DESIREDACCESS.FILE_SHARE_WRITE, 0, glbCls.CREATIONDISPOSITION.OPEN_EXISTING, glbCls.FLAGSANDATTRIBUTES.FILE_FLAG_OVERLAPPED, 0);
    }

    private static string GetSerialNumber(string deviceStr)
    {
      IntPtr device = CreateDeviceFile(deviceStr);

      if (device == new IntPtr(-1))
      {
        return "";
      }

      IntPtr serialBuff = Marshal.AllocHGlobal(256);
      bool boolResult = glbDll_B.GetIndexedString(device, 2, serialBuff, 256);
      string strSerialNum = Marshal.PtrToStringAuto(serialBuff);
      glbDll_B.CloseDeviceFile(device);
      return strSerialNum;
    }

    private static int GetNumUsbPort(ref List<string> listStr)
    {
      Guid HIDGuid = Guid.Empty;
      device_list.intCount = 0;
      glbDll_B.GetDeviceGuid(ref HIDGuid);//获取HID的全局GUID
      IntPtr HIDInfoSet = glbDll_B.GetClassDevOfHandle(HIDGuid);//获取包含所有HID接口信息集合的句柄

      if (HIDInfoSet != IntPtr.Zero)
      {
        glbCls.SP_DEVICE_INTERFACE_DATA interfaceInfo = new glbCls.SP_DEVICE_INTERFACE_DATA();
        interfaceInfo.cbSize = (uint)Marshal.SizeOf(interfaceInfo);

        //检测集合的每个接口
        for (uint index = 0; index < MAX_USB_DEVICES; index++)
        {
          //获取接口信息
          if (!glbDll_B.GetEnumDeviceInterfaces(HIDInfoSet, ref HIDGuid, index, ref interfaceInfo))
            continue;
          // 确保使用合适的数据类型
          IntPtr pDetail = IntPtr.Zero;
          int buffsize = 0;
          //获取接口详细信息；第一次读取错误，但可取得信息缓冲区的大小
          glbDll_B.GetDeviceInterfaceDetail(HIDInfoSet, ref interfaceInfo, IntPtr.Zero, ref buffsize);

          //接受缓冲
          pDetail = Marshal.AllocHGlobal((int)buffsize);
          var detail = new glbCls.SP_DEVICE_INTERFACE_DETAIL_DATA();
          detail.cbSize = (uint)Marshal.SizeOf(typeof(glbCls.SP_DEVICE_INTERFACE_DETAIL_DATA));
          Marshal.StructureToPtr(detail, pDetail, false);
          if (glbDll_B.GetDeviceInterfaceDetail(HIDInfoSet, ref interfaceInfo, pDetail, ref buffsize))//第二次读取接口详细信息
          {
            string strPid = "";
            string strVid = "";
            try
            {
              strPid = pID_.ToString("x4");
              strVid = vID_.ToString("x4");
              string strAll = "hid#vid_" + strVid + "&pid_" + strPid;
              string strHidDetail = Marshal.PtrToStringAuto((IntPtr)((int)pDetail + 4));
              if (strHidDetail.IndexOf(strAll) > 0)
              {
                device_list.deviceList[device_list.intCount] = strHidDetail;
                device_list.deviceSerialNameList[device_list.intCount] = GetSerialNumber(strHidDetail);
                //deviceList.Add(strHidDetail);
                //deviceSerialNameList.Add(GetSerialNumber(strHidDetail));
                listStr.Add(device_list.deviceSerialNameList[device_list.intCount]);
                device_list.intCount++;
              }
            }
            catch
            {
            }
            //deviceList.Add(Marshal.PtrToStringAuto((IntPtr)((int)pDetail + 4)));
          }
          else
          {
            int errorCode = Marshal.GetLastWin32Error();
            var sErr = $"GetEnumDeviceInterfaces failed with error code: {errorCode}";
            Console.WriteLine(sErr);
          }
          Marshal.FreeHGlobal(pDetail);
        }
      }
      else
        return 0;

      //删除设备信息并释放内存
      glbDll_B.DestroyDeviceInfoList(HIDInfoSet);

      return device_list.intCount;
    }
  }
}