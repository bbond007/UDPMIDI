using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Drawing.Drawing2D;

namespace UDPMIDI
{
    public partial class FormBB7UDPMIDI : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MidiOutCaps
        {
            public UInt16 wMid;
            public UInt16 wPid;
            public UInt32 vDriverVersion;

            [MarshalAs(UnmanagedType.ByValTStr,
               SizeConst = 32)]
            public String szPname;

            public UInt16 wTechnology;
            public UInt16 wVoices;
            public UInt16 wNotes;
            public UInt16 wChannelMask;
            public UInt32 dwSupport;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MidiHdr
        {
            public IntPtr lpData;          // offset  0- 3
            public uint dwBufferLength;    // offset  4- 7
            public uint dwBytesRecorded;   // offset  8-11
            public uint dwUser;            // offset 12-15
            public uint dwFlags;           // offset 16-19
            public IntPtr lpNext;          // offset 20-23
            public IntPtr reserved;        // offset 24-27
            public uint dwOffset;          // offset 28-31
            public IntPtr dwReserved;      // offset 32-35
        }

        class MidiHdrFlag
        {
            public const int MHDR_DONE = 1;
            public const int MHDR_PREPARED = 2;
            public const int MHDR_INQUEUE = 4;
            public const int MHDR_ISSTRM = 8;
        }

        // MCI INterface
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command,
        StringBuilder returnValue, int returnLength,
        IntPtr winHandle);

        // Midi API
        [DllImport("winmm.dll")]
        private static extern int midiOutGetNumDevs();

        [DllImport("winmm.dll")]
        private static extern int midiOutGetDevCaps(Int32 uDeviceID,
           ref MidiOutCaps lpMidiOutCaps, UInt32 cbMidiOutCaps);

        [DllImport("winmm.dll")]
        private static extern int midiOutOpen(ref int handle,
           int deviceID, MidiCallBack proc, int instance, int flags);

        [DllImport("winmm.dll")]
        private static extern int midiOutShortMsg(int handle,
           int message);

        [DllImport("winmm.dll")]
        private static extern int midiOutClose(int handle);

        private delegate void MidiCallBack(int handle, int msg,
          int instance, int param1, int param2);

        [DllImport("winmm.dll")]
        static extern int midiOutLongMsg(int hMidiOut, ref MidiHdr lpMidiOutHdr, int uSize);

        [DllImport("winmm.dll")]
        static extern int midiOutPrepareHeader(int hMidiOut, ref MidiHdr lpMidiOutHdr, int uSize);

        [DllImport("winmm.dll")]
        static extern int midiOutUnprepareHeader(int hMidiOut, ref MidiHdr lpMidiOutHdr, int uSize);

        public FormBB7UDPMIDI()
        {
            InitializeComponent();
        }

        const string MT32_Message = "<<<Binary Bond007>>>";

        void SendLongMsg(int systemHandle, byte[] data)
        {
            if (data.Length > (64 * 1024))
            {
                throw new ArgumentOutOfRangeException();
            }
            //----------// MidiHdr
            int hdrSize = Marshal.SizeOf(typeof(MidiHdr));
            byte[] hdrReserved = new byte[8];
            MidiHdr hdr = new MidiHdr();
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            GCHandle revHandle = GCHandle.Alloc(hdrReserved, GCHandleType.Pinned);
            //----------// MidiHdr
            hdr.lpData = handle.AddrOfPinnedObject();
            hdr.dwBufferLength = (uint)data.Length;
            hdr.dwFlags = 0;
            //----------//
            midiOutPrepareHeader(systemHandle, ref hdr, hdrSize);
            while ((hdr.dwFlags & MidiHdrFlag.MHDR_PREPARED) != MidiHdrFlag.MHDR_PREPARED)
            {
                Thread.Sleep(1);
            }
            midiOutLongMsg(systemHandle, ref hdr, hdrSize);
            while ((hdr.dwFlags & MidiHdrFlag.MHDR_DONE) != MidiHdrFlag.MHDR_DONE)
            {
                Thread.Sleep(1);
            }
            midiOutUnprepareHeader(systemHandle, ref hdr, hdrSize);
            //----------// GCHandle
            handle.Free();
            revHandle.Free();
            //----------//
        }

        void OnUdpData(IAsyncResult result)
        {
            UdpClient socket = result.AsyncState as UdpClient;
            // points towards whoever had sent the message:
            IPEndPoint source = new IPEndPoint(0, 0);
            byte[] message = socket.EndReceive(result, ref source);
            SendLongMsg(midiHandle, message);

            Invoke((MethodInvoker)delegate
            {
                try
                {
                    textBoxPacketsRec.Text = inCount++.ToString();
                }
                catch (Exception OOPS)
                { }
            });

            socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
        } 

        int midiHandle = 0;
        int inCount = 0;

        private void OpenMIDIDevice()
        {
            if (midiHandle != 0)
                midiOutClose(midiHandle);
            int result = midiOutOpen(ref midiHandle, comboBoxMidiDevice.SelectedIndex, null, 0, 0);
            SetMT32_LCD(MT32_Message);
        }

        private void FormBB7UDPMIDI_Load(object sender, EventArgs e)
        {
            var numDevs = midiOutGetNumDevs();
            if (numDevs > 0)
            {
                for (int i = 0; i < numDevs; i++)
                {
                    MidiOutCaps myCaps = new MidiOutCaps();
                    int result2 = midiOutGetDevCaps(i, ref myCaps, (UInt32)Marshal.SizeOf(myCaps));
                    comboBoxMidiDevice.Items.Add(string.Format("{0} - {1}", i, myCaps.szPname));
                }
                if (numDevs > 0)
                    comboBoxMidiDevice.SelectedIndex = numDevs - 1;

                UdpClient socket = new UdpClient(int.Parse(textBoxUDPPort.Text));
                IPAddress multicastaddress = IPAddress.Parse("239.0.0.1");
                socket.JoinMulticastGroup(multicastaddress);
                socket.BeginReceive(new AsyncCallback(OnUdpData), socket); 
            }
            else
            {
                MessageBox.Show("ERROR : NO MIDI DEVICES FOUND");
            }
            textBoxPacketsRec.ReadOnly = true;
        }

        private void SetMT32_LCD(string MT32Message)
        {
            byte[] buf = {0xF0, 0x41, 0x10, 0x16, 0x12, 0x20, 0x00, 0x00,
                          0,0,0,0,0, //sysex character data
                          0,0,0,0,0, // "
                          0,0,0,0,0, // "
                          0,0,0,0,0, // "
                          0x00, /* checksum placedholder */
                          0xF7  /* end of sysex */ };
            int checksum = 0;
            int MT32MessageIndex = 0;
            for (int bufIndex = 5; bufIndex < buf.Length - 2; bufIndex++)
            {
                if (bufIndex > 7)
                {
                    if (MT32MessageIndex < MT32Message.Length)
                        buf[bufIndex] = (byte)MT32Message[MT32MessageIndex++];
                    else
                        buf[bufIndex] = 0x20;
                }
                checksum += buf[bufIndex];
            }
            checksum = 128 - checksum % 128;
            buf[buf.Length - 2] = (byte)(checksum);
            SendLongMsg(midiHandle, buf);
        }

        private void FormBB7UDPMIDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (midiHandle != 0)
                midiOutClose(midiHandle);
        }

        private void comboBoxMidiDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenMIDIDevice();
        }

        private void FormBB7UDPMIDI_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                              Color.WhiteSmoke,
                                                              Color.SteelBlue,
                                                              90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            labelTitle.Refresh();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            inCount = 0;
            textBoxPacketsRec.Text = "0";
            textBoxPacketsSent.Text = "0";
            SetMT32_LCD(MT32_Message);
        }
    }
}
