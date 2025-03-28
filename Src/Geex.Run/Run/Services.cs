
// Type: Geex.Run.Services
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using Geex.Edit;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace Geex.Run
{
  public sealed class Services
  {
    private static bool isTrialMode = true;
    private static string clipboard = "";

    public static bool IsGamePurchased(string gameName)
    {
      try
      {
        return Storage.IsFileExist(gameName + "LicenseKey.geex") && Services.IsLicenseFileValid(Storage.LoadLicenseFile());
      }
      catch
      {
        return false;
      }
    }

    public static bool RegisterLicense(string licenseKey)
    {
      if (!Services.IsLicenseKeyCorrect(licenseKey))
      {
        Services.ShowMessage("Error", "This Key : " + licenseKey + " is incorrect. Please retry, or contact your Game Provider");
        return false;
      }
      if (GeexEdit.IsLicenseWithGeexServerCheck && !Services.IsLicenseAvailableOnServer(licenseKey))
        return false;
      return Storage.SaveLicenseFile(new LicenseData()
      {
        OriginalLicense = licenseKey,
        LicenseCode = Services.GenerateCodeFromLicense(licenseKey)
      });
    }

    private static bool IsLicenseFileValid(LicenseData licenseData)
    {
      if (licenseData.LicenseCode.Length != 15)
        return false;
      string localEncryptionCode = Services.GetLocalEncryptionCode();
      string licenseCode = licenseData.LicenseCode;
      for (int index = 1; index < licenseData.LicenseCode.Length; ++index)
      {
        if ((int) licenseCode[index] == (int) (ushort) ((uint) licenseData.OriginalLicense[index] + (uint) localEncryptionCode[index]))
          return false;
      }
      return true;
    }

    internal static string GenerateCodeFromLicense(string licenseKey)
    {
      string localEncryptionCode = Services.GetLocalEncryptionCode();
      string empty = string.Empty;
      for (int index = 0; index < 15; ++index)
        empty += ((char) ((uint) licenseKey[index] + (uint) localEncryptionCode[index])).ToString();
      return empty.ToString();
    }

    private static string GetLocalEncryptionCode()
    {
      string imac = Services.GetImac();
      while (imac.Length < 15)
        imac += "A";
      return imac.Substring(0, 15);
    }

    private static bool RegisterLicenseOnServer(string license)
    {
      try
      {
        return new WebClient().DownloadString("https://www.plimus.com/jsp/validateKey.jsp?action=VALIDATE&productId=794840&key=" + license).Contains("SUCCESS");
      }
      catch
      {
        Services.ShowMessage("Error", "Connection Problem with Serial check Server. Check your a firewall or internet connection");
        return false;
      }
    }

    internal static bool UnRegisterLicenseOnServer(string license)
    {
      try
      {
        return new WebClient().DownloadString("https://www.plimus.com/jsp/validateKey.jsp?action=UNREGISTER&productId=794840&key=" + license).Contains("SUCCESS");
      }
      catch
      {
        Services.ShowMessage("Error", "Connection Problem with Serial check Server. Check your a firewall or internet connection");
        return false;
      }
    }

    public static bool IsLicenseKeyCorrect(string key)
    {
      if (GeexEdit.IsLicenseWithGeexServerCheck)
        return Services.IsLicenseAvailableOnServer(key);
      string licenseMagicNumber = GeexEdit.LicenseMagicNumber;
      for (int index = 3; index < key.Length - 1; ++index)
      {
        int num1 = (int) key[index - 1] - 65;
        int num2 = (int) licenseMagicNumber[index - 3] - 65;
        int num3 = (int) key[index] - 65;
        int num4 = num2;
        if ((num1 + num4) % 26 != num3)
          return false;
      }
      return true;
    }

    private static bool IsLicenseAvailableOnServer(string license)
    {
      try
      {
        if (new WebClient().DownloadString("https://www.plimus.com/jsp/validateKey.jsp?action=REGISTER&productId=794840&key=" + license).Contains("SUCCESS"))
          return true;
        Services.ShowMessage("Error", "This Key : " + license + " is used already.");
        return false;
      }
      catch
      {
        Services.ShowMessage("Error", "Connection Problem with Serial check Server. Check your a firewall or internet connection");
        return false;
      }
    }

    internal static string GetImac()
    {
      try
      {
        IPGlobalProperties globalProperties = IPGlobalProperties.GetIPGlobalProperties();
        NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        Console.WriteLine("Interface information for {0}.{1}     ", (object) globalProperties.HostName, (object) globalProperties.DomainName);
        return networkInterfaces[0].GetPhysicalAddress().ToString();
      }
      catch
      {
        return "GEEXPOWEREDTOOL";
      }
    }

    public static string GetClipBoardText()
    {
      Thread thread = new Thread(new ThreadStart(Services.getClipboard));
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      do
        ;
      while (thread.IsAlive);
      return Services.clipboard;
    }

    [STAThread]
    private static void getClipboard()
    {
      if (!Clipboard.ContainsText())
        return;
      Services.clipboard = Clipboard.GetText(TextDataFormat.UnicodeText);
    }

    public static bool IsTrialMode => Services.isTrialMode;

    public static void IsTrialModeRefresh()
    {
      Services.isTrialMode = !Services.IsGamePurchased(GeexEdit.GameTitle);
    }

    public static void ShowWarningMessage(string message)
    {
      int num = (int) MessageBox.Show(message);
    }

    public static void ShowMessage(string title, string message)
    {
      int num = (int) MessageBox.Show(message);
    }

    public static void ShowMarketplace()
    {
      Process.Start((string) new XmlSerializer(typeof (string)).Deserialize((Stream) System.IO.File.Open("buy.xml", FileMode.Open)));
    }
  }
}
