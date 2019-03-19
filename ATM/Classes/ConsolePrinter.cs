﻿using System;
using System.Collections.Generic;
using System.Linq;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class ConsolePrinter : IConsolePrinter
    {
        /// <summary>
        /// Receives a List of tracks currently in the airspace, and tracks in conflict by 
        /// </summary>
        /// <param name="tracks">Tracks currently in the airspace</param>
        /// <param name="conflictTags">Tracks in breaking separation condition in the airspace</param>
        public void Print(List<Track> tracks, List<string> conflictTags) 
        {
            //Console.Clear(); //Uncomment this when not unittesting


            if (!tracks.Any())
            {
                Console.WriteLine("No airplanes currently in the airspace");
                return;
            }

            Console.WriteLine("Airplane Tracks currently in the airspace:");

            foreach (var track in tracks)
            {
                if (track.Velocity != null && track.CurrentCompCourse != null) //Only display if it has velocity and compcourse
                    Console.WriteLine($"Flight tag: {track.Tag} " +
                                      $"X: {track.XCoordinate} " +
                                      $"Y: {track.YCoordinate} " +
                                      $"Altitude: {track.Altitude} " +
                                      $"Horizontal Velocity: {track.Velocity} " +
                                      $"Compass Course: {track.CurrentCompCourse}");
            }
            Console.WriteLine();

            if (conflictTags.Count == 0)
            {
                Console.WriteLine("No Airplanes in conflict");
                return;
            }

            //foreach(conflict in conflicts)
            //{
            //    Console.WriteLine("Airplanes in conflict");
                
            //    Console.WriteLine($"Airplane tag: {con}");
            //    Console.WriteLine($"Airplane tag: {tag}");
                
            //}
        }

        public void ConsoleSeparationDataHandler(object sender, ConsoleSeparationEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
