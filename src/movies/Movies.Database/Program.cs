﻿namespace Movies.Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Upgrader.Upgrade(args.FirstOrDefault()!);
        }
    }
}