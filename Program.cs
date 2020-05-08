using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace MusicPlaylistAnalyzer {
    
    class Program {
        
        static void Main (string[] args) {
            string data=null;
            int index;
            List<Tune>SongList = new List<Tune>();
            
            try {
                
                if (File.Exists($"SampleMusicPlaylist.txt")==false) {
                    Console.WriteLine("File not found!");
                }
                else {
                    StreamReader sr = new StreamReader ($"SampleMusicPlaylist.txt");
                    index=0;
                    string song = sr.ReadLine();
                    while ((song=sr.ReadLine())!= null) {
                        index += 1;
                        try {
                            string[] strings = song.Split('\t');
                            if (strings.Length < 8) {
                                Console.Write("Record has incorrect # of elements");
                                Console.Write($"Row {index} Contains {strings.Length} values. It should have 8.");
                                break;
                            }
                            else {
                                Tune dataTemp = new Tune(
                                    (strings[0]), 
                                    (strings[1]), 
                                    (strings[2]), 
                                    (strings[3]), 
                                    Int32.Parse(strings[4]), 
                                    Int32.Parse(strings[5]), 
                                    Int32.Parse(strings[6]), 
                                    Int32.Parse(strings[7]));
                                SongList.Add(dataTemp);

                            }
                        }
                        catch (Exception e) {
                            Console.Write("Error");
                            break;
                        }
                        sr.Close();
                    }
                }
                
                
            }
            catch (Exception e) {
                Console.Write("Error");
            }
            
            try {
                Tune[] songs = SongList.ToArray();

                using (StreamWriter write = new StreamWriter("MusicPlaylistReport.txt")) {
                    write.WriteLine("Music Playlist Report");

                    var SongsPlays = from song in songs where song.Plays >= 200 select song;

                    data += "\nSongs That Received 200 Or More Plays: \n";

                    foreach (Tune song in SongsPlays) data += song + "\n";


                    var SongsGenreAlternative = from song in songs where song.Genre == "Alternative" select song;
                    data += $"Number Of Songs That Are In The Playlist With The Genre Of Alternative: {SongsGenreAlternative.Count()}\n";


                    var SongsGenreHipHopRap = from song in songs where song.Genre == "Hip-Hop/Rap" select song;
                    data += $"Number Of Songs That Are In The PlayList With The Genre Of Hip-Hop/Rap: {SongsGenreHipHopRap.Count()} \n";


                    var SongsAlbumFishbowl = from song in songs where song.Album == "Welcome to the Fishbowl" select song;
                    data += "\nSongs That Are In The Playlist From The Album Welcome To The Fishbowl: \n";
                    foreach (Tune song in SongsAlbumFishbowl) data += song + "\n";


                    var Songs1970 = from song in songs where song.Release < 1970 select song;
                    data += "\nSongs That Are In The Playlist From Before 1970:\n";
                    foreach (Tune song in Songs1970) data += song + "\n";


                    var Names85Characters = from song in songs where song.Title.Length > 85 select song.Title;
                    data += "Songs That Are More Than 85 Characters Long:\n";
                    foreach (string name in Names85Characters) data += name + "\n";


                    var LongestSong = from song in songs orderby song.Length descending select song;
                    data += "\nLongest Song: ";
                    data += LongestSong.First();
                    data += "\n";

                    write.Write(data);

                    write.Close();
                }

                Console.WriteLine("\nPlaylist Created");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nFile write error");
                Console.WriteLine("{0}", e);
            }

            Console.ReadLine();
        
    
        }
    }
    class Tune {
        public int Length;
        public int Size;
        public int Release;
        public int Plays;

        public string Title, Album, Artist, Genre;

        public Tune (string title, string album, string artist, string genre, int length, int size, int release, int plays) {
            Title=title;
            Album=album;
            Artist=artist;
            Genre=genre;
            Length=length;
            Size=size;
            Release=release;
            Plays=plays;
        }
    }



}