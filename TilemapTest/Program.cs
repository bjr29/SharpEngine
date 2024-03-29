﻿using SharpEngine;
using System;

namespace UITest {
    static class Program {
        public const string ImagesPath = "Images/";

        public static Tilemap Tilemap { get; set; } = new();

        static void Main(string[] args) {
            Engine.Ready += Ready;

            Engine.Init();
        }

        private static void Ready(object? sender, EventArgs e) {
            Engine.Window = new();

            Tilemap.Tileset = new() {
                new(ImagesPath + "1.png") {
                    ColourMod = new(255, 0, 0)
                },
                new(ImagesPath + "2.png") {
                    ColourMod = new(0, 255, 0)
                },
                new(ImagesPath + "3.png") {
                    ColourMod = new(0, 0, 255)
                },
            };

            Tilemap.Tiles = new() {
                new(0, new(0, 0)),
                new(1, new(1, 0)),
                new(2, new(2, 0)),
                new(1, new(0, 1)),
                new(2, new(1, 1)),
                new(0, new(2, 1)),
                new(2, new(0, 2)),
                new(0, new(1, 2)),
                new(1, new(2, 2)),
            };
        }
    }
}