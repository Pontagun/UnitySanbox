using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using UnityEditor;
using System.Text;
using System.Collections.Generic;

namespace Cubemos
{
    /// <summary>
    /// Minimal sample demonstrating the use of Cubemos Skeleton Tracking with Intel Realsense D415/D435
    /// </summary>
    public class HelloCubemos : MonoBehaviour
    {
        private SkeletonTracker _skeletonTracker;
        private RealsenseManager _realsense;
        public List<Skeleton> lastSkeletons;
        

        void Start()
        {
            Debug.Log("Starting Cubemos Skeleton Tracking");
            
            //Initialise the cubemos skeleton tracking and intel realsense pipeline
           _skeletonTracker = new SkeletonTracker();
            _realsense = new RealsenseManager();

            _skeletonTracker.Initialize();
            _realsense.Initialize();
        }

        void Update() {
            byte[] read_bytes0 = new byte[56];   // <----------------- No. of Bytes
                                                 // Mono, for some reason, seems to randomly fail on the first read after a write so we must loop
                                                 // through to make sure the bytes are read and Mono also seems not to always read the amount asked
                                                 // so we must also read one byte at a time
            int read_counter = 100;
            int byte_idx0 = 0;
            //sp0.Write(stream_slots_bytes, 0, stream_slots_bytes.Length);

            //while (read_counter > 0)
            //{

            //    try
            //    {
            //        byte_idx0 += sp0.Read(read_bytes0, byte_idx0, 1);
            //    }
            //    catch
            //    {
            //        // Failed to read from serial port
            //    }
            //    if (byte_idx0 == 56)
            //    { // <----------------- No. of Bytes
            //        break;
            //    }
            //    if (read_counter <= 0)
            //    {
            //        throw new System.Exception("Failed to read quaternion from port too many times." +
            //            " This could mean the port is not open or the Mono serial read is not responding.");
            //    }
            //    --read_counter;
            //}

            //Debug.Log(bytesToFloat(read_bytes0, 16));
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
        //void Update()
        //{
        //    if (_realsense != null)
        //    {
        //        using (var frame = _realsense.GetFrame())
        //        {
        //            lastSkeletons = _skeletonTracker.TrackSkeletonsWithRealsenseFrames(frame.ColorFrame,
        //                                                                               frame.DepthFrame,
        //                                                                               _realsense.Intrinsics);
        //            if (lastSkeletons.Count == 1) {
        //                foreach (var sk in lastSkeletons)
        //                {
        //                    var sb = new System.Text.StringBuilder();
        //                    sb.AppendLine("<b>Skeleton " + sk.Index + "</b>");

        //                    foreach (var j in sk.Joints)
        //                    {
        //                        Console.WriteLine(j.Value.confidence.ToString("F2"));
        //                        if (j.Key == 7) {
        //                            if (float.Parse(j.Value.confidence.ToString("F2")) > 0.2) {
        //                                transform.position = new Vector3(-j.Value.position.x * 10f + 2, -j.Value.position.y * 10f + 2, j.Value.position.z * 10f);
        //                            }
        //                        }
        //                        //sb.AppendLine("Joint " + j.Key + ": " + j.Value.position + ", confidence: " + j.Value.confidence.ToString("F2"));
        //                        Debug.Log(float.Parse(j.Value.confidence.ToString("F2")) > 0.01);
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("RealSense pipeline not initialized!");
        //    }
        //}
    }
}