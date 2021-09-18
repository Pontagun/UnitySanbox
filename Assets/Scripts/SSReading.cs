using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityScript.Lang;
using System.Text;
public class SSReading : MonoBehaviour
{
    private static SerialPort sp0;
    private SSConfigurations conf;
    private string _portName = "";

    public static byte[] stream_timing_bytes = new byte[15];
    public static byte[] interval = BitConverter.GetBytes(70000);
    public static byte[] delay = BitConverter.GetBytes(0);
    public static byte[] duration = BitConverter.GetBytes(0xFFFFFFFF);

    private byte[] stream_slots_bytes = SSConfigurations.stream_slots_bytes;
    public static byte[] start_stream_bytes = new byte[3];

    void Start()
    {
        string[] ports = SerialPort.GetPortNames();
        
        if (ports.Length == 1)
        {
            _portName = ports[0];
        }
        else
        {
            print("Please manually adds port number.");
        }

        sp0 = new SerialPort(_portName, 115200, Parity.None, 8, StopBits.One);

        sp0.WriteTimeout = 500;
        sp0.ReadTimeout = 500;

        if (!sp0.IsOpen)
        {
            try
            {
                sp0.Open();
                Console.WriteLine("Serial Port 0 is open (COM7)");

                sp0.Write(stream_slots_bytes, 0, stream_slots_bytes.Length);

                System.Array.Reverse(interval);
                System.Array.Reverse(delay);
                System.Array.Reverse(duration);

                interval.CopyTo(stream_timing_bytes, 2);
                delay.CopyTo(stream_timing_bytes, 6);
                duration.CopyTo(stream_timing_bytes, 10);
                stream_timing_bytes[14] = (byte)((stream_timing_bytes[1] + stream_timing_bytes[2] + stream_timing_bytes[3] + stream_timing_bytes[4] + stream_timing_bytes[5] + stream_timing_bytes[6] +
                stream_timing_bytes[7] + stream_timing_bytes[8] + stream_timing_bytes[9] + stream_timing_bytes[10] + stream_timing_bytes[11] + stream_timing_bytes[12] + stream_timing_bytes[13]) % 256);

                sp0.Write(stream_timing_bytes, 0, stream_timing_bytes.Length);

                start_stream_bytes[0] = SSConfigurations.TSS_START_BYTE;
                start_stream_bytes[1] = SSConfigurations.TSS_START_STREAMING;
                start_stream_bytes[2] = SSConfigurations.TSS_START_STREAMING;

                sp0.Write(start_stream_bytes, 0, start_stream_bytes.Length);


                //sp0.Open();
                //print("Serial connection is starting at " + _portName);
                //print("Serial connection starts at " + _portName);

                //sp0.Write(SSConfigurations.stream_slots_bytes, 0, SSConfigurations.stream_slots_bytes.Length);
            }
            catch (Exception e)
            {
                print("Serial connection can not start at " + _portName);
            }
        }
        else
        {
            Debug.LogError("All Serial Ports are already open.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        byte[] read_bytes0 = new byte[56];   // <----------------- No. of Bytes
                                             // Mono, for some reason, seems to randomly fail on the first read after a write so we must loop
                                             // through to make sure the bytes are read and Mono also seems not to always read the amount asked
                                             // so we must also read one byte at a time
        int read_counter = 100;
        int byte_idx0 = 0;

        while (read_counter > 0)
        {

            try
            {
                byte_idx0 += sp0.Read(read_bytes0, byte_idx0, 1);
            }
            catch
            {
                // Failed to read from serial port
            }
            if (byte_idx0 == 56)
            { // <----------------- No. of Bytes
                break;
            }
            if (read_counter <= 0)
            {
                throw new System.Exception("Failed to read quaternion from port too many times." +
                    " This could mean the port is not open or the Mono serial read is not responding.");
            }
            --read_counter;
        }

        print(bytesToFloat(read_bytes0, 4));
    }

    void print(string txt)
    {
        Debug.Log(txt);
    }

    float bytesToFloat(byte[] raw_bytes, int offset)
    {
        byte[] big_bytes = new byte[4];
        big_bytes[0] = raw_bytes[offset + 3];
        big_bytes[1] = raw_bytes[offset + 2];
        big_bytes[2] = raw_bytes[offset + 1];
        big_bytes[3] = raw_bytes[offset + 0];
        return BitConverter.ToSingle(big_bytes, 0);
    }
}
