using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// See page: http://msdn.microsoft.com/en-us/library/cc231198.aspx
namespace HResultDecoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint hresult = 0x0;
            try
            {
                hresult = Convert.ToUInt32(hresultInput.Text, 16);
            }
            catch (Exception)
            {
                decodedValue.Text = "That's not a valid HResult, fuck you.";
                return;
            }

            bool severity = IsBitSet(hresult, 0);
            bool reservedR = IsBitSet(hresult, 1);
            bool customer = IsBitSet(hresult, 2);
            bool ntstatus = IsBitSet(hresult, 3);
            bool reservedX = IsBitSet(hresult, 4);
            int facility = GetFacility(hresult);
            int code = GetCode(hresult);

            decodedValue.Text = string.Format(
@"Code Value: {7}
Facility Code: {5}
Facility Name: {6}

---- Header Bit Values ----
    Severity (S): {0}
    Reserved (R): {1}
    Customer (C): {2}
    NTStatus (N): {3}
    Reserved (X): {4}",
 severity ? "Failure" : "Success",
 reservedR ? "Set" : "Not Set",
 customer ? "Customer Code" : "Microsoft Code",
 ntstatus ? "Set" : "Not Set",
 reservedX ? "Set (Not Supposed To Happen)" : "Not Set",
 facility,
 GetFacilityName(facility),
 code
 );

        }

        private static bool IsBitSet(uint hresult, int bitIndex)
        {
            const uint zero = 0;
            return (hresult & (zero | (uint)Math.Pow(2, bitIndex))) > 0;
        }

        private static int GetFacility(uint hresult)
        {
            return (int)((hresult << 5) >> 21);
        }

        private static int GetCode(uint hresult)
        {
            return (int)((hresult << 16) >> 16);
        }

        private static string GetFacilityName(int facility)
        {
            switch (facility)
            {
                case 1:
                    return "RPC subsystem";
                case 2:
                    return "COM Dispatch";
                case 3:
                    return "OLE Storage";
                case 4:
                    return "COM/OLE Interface management";
                case 7:
                    return "Reserved for WIN32 internal crap";
                case 8:
                    return "Windows subsystem";
                case 9:
                    return "Security API layer";
                case 10:
                    return "Control mechanism";
                case 11:
                    return "Certificate or client server";
                case 12:
                    return "Wininet related";
                case 13:
                    return "Windows Media Server";
                case 14:
                    return "Microsoft Message Queue";
                case 15:
                    return "Setup API";
                case 16:
                    return "Smart-cars subsystem";
                case 17:
                    return "COM+";
                case 18:
                    return "Microsoft Agent";
                case 19:
                    return ".Net Common Language Runtime";
                case 20:
                    return "Audit collection service";
                case 21:
                    return "Direct Play";
                case 22:
                    return "Ubiquitous Memoryintrospection Service";
                case 23:
                    return "Side-by-side servicing";
                case 24:
                    return "Windows CE";
                case 25:
                    return "Http support";
                case 26:
                    return "Common Logging support";
                case 31:
                    return "User mode filter manager";
                case 32:
                    return "Background copy control";
                case 33:
                    return "Configuration service";
                case 34:
                    return "State Management service";
                case 35:
                    return "Microsoft Identity Server";
                case 36:
                    return "Windows Update";
                case 37:
                    return "Active Directory service";
                case 38:
                    return "Graphics drivers";
                case 39:
                    return "User Shell";
                case 40:
                    return "Trusted Platform Module service";
                case 41:
                    return "Trusted Platofrm Module applications";
                case 48:
                    return "Perfomance Logs and Alerts";
                case 49:
                    return "Full volume encryption";
                case 50:
                    return "Firewall Platform";
                case 51:
                    return "Windows Resource Manager";
                case 52:
                    return "Network Driver Interface";
                case 53:
                    return "Usermode Hypervisor components";
                case 54:
                    return "Configuration Managemeent Interface";
                case 55:
                    return "Usermode virtualization subsystem";
                case 56:
                    return "Usermode volume manager";
                case 57:
                    return "Boot Configuration Database";
                case 58:
                    return "Usermode virtual hard disk support";
                case 60:
                    return "System Diganostics";
                case 61:
                    return "Web Services";
                case 80:
                    return "A Windows Defender component";
                case 81:
                    return "Open Connectivity Service";
                default:
                    return "Unknown (not listed in MSDN documentation)";
            }
        }
    }
}
