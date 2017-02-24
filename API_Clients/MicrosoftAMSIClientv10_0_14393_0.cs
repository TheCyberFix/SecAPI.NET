using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace SecAPI.API_Clients
{

    //https://blogs.technet.microsoft.com/mmpc/2015/06/09/windows-10-to-offer-application-developers-new-malware-defenses/
    //http://cn33liz.blogspot.com/2016/05/bypassing-amsi-using-powershell-5-dll.html
    //Orig file: https://github.com/adamdriscoll/AMSI
    /// <summary>
    /// Client class used to access the Microsoft Antimalware Scan Interface currently implemented on Windows 10+
    /// 
    /// From MSDN: The Antimalware Scan Interface is designed for use by two groups of developers:
    ///  - App developers who want to make requests to antimalware products from within their apps.
    ///  - Third-party creators of antimalware products who want their products to offer the best features to apps. 
    /// 
    /// </summary>
    public class MicrosoftAMSIClientv10_0_14393_0 : IDisposable
    {

        private IntPtr amsiContext;
        private IntPtr amsiSession;
        private AMSI_RESULT amsiResult;

        // Flag: Has Dispose already been called?
        bool disposed = false;


        /// <summary>
        /// Initializes the Cient and opens a new session (for single or multiple fragment) scanning.
        /// </summary>
        /// <param name="appName"></param>
        public MicrosoftAMSIClientv10_0_14393_0(string appName)
        {

            NativeMethods.AmsiInitialize(appName, out amsiContext);
            NativeMethods.AmsiOpenSession(amsiContext, out amsiSession);

        }


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                //Clean up
                NativeMethods.AmsiCloseSession(amsiContext, amsiSession);
                NativeMethods.AmsiUninitialize(amsiContext);

            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }



        /// <summary>
        /// Scans a string for malware.
        /// </summary>
        /// <param name="strToScan">The string to be scanned.</param>
        /// <param name="meaningfulContentName">The filename, URL, unique script ID, or similar of the content being scanned.</param>
        public AMSI_RESULT scanString(string strToScan, string meaningfulContentName)
        {

            NativeMethods.AmsiScanString(amsiContext, strToScan, meaningfulContentName, amsiSession, out amsiResult);
            return amsiResult;

        }


        /// <summary>
        /// Scans a buffer full of content for malware.
        /// </summary>
        /// <param name="buffer">The buffer from which to read the data to be scanned.</param>
        /// <param name="meaningfulContentName">The filename, URL, unique script ID, or similar of the content being scanned.</param>
        public AMSI_RESULT scanByteArray(byte[] buffer, string meaningfulContentName)
        {

            NativeMethods.AmsiScanBuffer(amsiContext, buffer, Convert.ToUInt32(buffer.Length), meaningfulContentName, amsiSession, out amsiResult);
            return amsiResult;

        }


        /// <summary>
        /// These appear to be based on the values that Windows Defender returns.  So, they may be specific to that single Antimalware program.
        /// </summary>
        public enum AMSI_RESULT
        {
            AMSI_RESULT_CLEAN = 0,
            AMSI_RESULT_NOT_DETECTED = 1,
            AMSI_RESULT_DETECTED = 32768
        }

        private static class NativeMethods
        {
            [DllImport("Amsi.dll", EntryPoint = "AmsiInitialize", CallingConvention = CallingConvention.StdCall)]
            public static extern int AmsiInitialize([MarshalAs(UnmanagedType.LPWStr)]string appName, out IntPtr amsiContext);

            [DllImport("Amsi.dll", EntryPoint = "AmsiUninitialize", CallingConvention = CallingConvention.StdCall)]
            public static extern void AmsiUninitialize(IntPtr amsiContext);

            [DllImport("Amsi.dll", EntryPoint = "AmsiOpenSession", CallingConvention = CallingConvention.StdCall)]
            public static extern int AmsiOpenSession(IntPtr amsiContext, out IntPtr session);

            [DllImport("Amsi.dll", EntryPoint = "AmsiCloseSession", CallingConvention = CallingConvention.StdCall)]
            public static extern void AmsiCloseSession(IntPtr amsiContext, IntPtr session);


            /// <summary>
            /// Scans a string for malware.
            /// </summary>
            /// <param name="amsiContext">The handle of type HAMSICONTEXT that was initially received from AmsiInitialize.</param>
            /// <param name="string">The string to be scanned.</param>
            /// <param name="contentName">The filename, URL, unique script ID, or similar of the content being scanned.</param>
            /// <param name="session">If multiple scan requests are to be correlated within a session, set session to the handle of type HAMSISESSION that was initially received from AmsiOpenSession. Otherwise, set session to nullptr.</param>
            /// <param name="result">The result of the scan. See AMSI_RESULT.</param>
            /// <returns></returns>
            [DllImport("Amsi.dll", EntryPoint = "AmsiScanString", CallingConvention = CallingConvention.StdCall)]
            public static extern int AmsiScanString(IntPtr amsiContext, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPWStr)]string @string, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPWStr)]string contentName, IntPtr session, out AMSI_RESULT result);



            /// <summary>
            /// Scans a buffer full of content for malware.
            /// </summary>
            /// <param name="amsiContext">The handle of type HAMSICONTEXT that was initially received from AmsiInitialize.</param>
            /// <param name="buffer">The buffer from which to read the data to be scanned.</param>
            /// <param name="length">The length, in bytes, of the data to be read from buffer.</param>
            /// <param name="contentName">The filename, URL, unique script ID, or similar of the content being scanned.</param>
            /// <param name="session">If multiple scan requests are to be correlated within a session, set session to the handle of type HAMSISESSION that was initially received from AmsiOpenSession. Otherwise, set session to nullptr.</param>
            /// <param name="result">The result of the scan. See AMSI_RESULT.</param>
            /// <returns></returns>
            [DllImport("Amsi.dll", EntryPoint = "AmsiScanBuffer", CallingConvention = CallingConvention.StdCall)]
            public static extern int AmsiScanBuffer(IntPtr amsiContext, byte[] buffer, uint length, string contentName, IntPtr session, out AMSI_RESULT result);


            /// <summary>
            /// This method exists on MSDN but not in AMSI.dll (v10_0_14393_0): "ERROR Unable to find an entry point named 'AmsiResultIsMalware' in DLL 'Amsi.dll'."
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            //[DllImport("Amsi.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
            //public static extern bool AmsiResultIsMalware(AMSI_RESULT result);
        }

    }
}
