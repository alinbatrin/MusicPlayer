using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace MusicPlayer
{
	public partial class MainPage : ContentPage
	{
        ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        private Label _CronoTime;
        private Label _SongTime;
        private Slider _Slider;

        private bool _stopTime;
        private double _numberLoops = 0;
        private bool _fromTimer;

        public MainPage()
        {
            InitializeComponent();

            //Alin B - Initialize labels.
            _CronoTime = this.FindByName<Label>("CronoTime");
            _SongTime = this.FindByName<Label>("SongTime");
            _Slider = this.FindByName<Slider>("Slider");
            //Alin B - Player load my default song.
            player.Load("M1.mp3");
        }


        void Play_Button_Clicked(object sender, EventArgs e)
        {
            //Alin B - Play my default song.
            //Alin B - TODO : Specificatii cand dau play stiu cat dureaza melodia.
            player.Play();

            //Cand se da prima data play trebuie sa fie pus _fromTimer pe true
            //Ca sa nu inceapa melodia de 2 ori
            _fromTimer = true;
            _stopTime = false;
            _SongTime.Text = Return_Time(player.Duration);


            //Alin B - Timer - TODO : needs to be refactored in a function
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                if (_stopTime) return false;


                Device.BeginInvokeOnMainThread(() => 
                {
                    _CronoTime.Text = Return_Time(_numberLoops++);
                    _Slider.Value = (_numberLoops*100)/player.Duration;
                    _fromTimer = true;
                });


                if (_numberLoops < player.Duration)
                    return true;
                else
                    return false;

                
            });
        }

        void Pause_Button_Clicked(object sender, EventArgs e)
        {
            //Alin B - Pause my default song.
            player.Pause();
        }

        void Stop_Button_Clicked(object sender, EventArgs e)
        {
            //Alin B - Pause my default song.
            _stopTime = true;
            player.Stop();
            _Slider.Value = 0;
            _CronoTime.Text = Return_Time(0);
            _SongTime.Text = Return_Time(0);
        }

        //Alin B - Slider has 100 max value
        //Alin B - _songPoint este locul din melodie care s-a selectat cu sliderul
        void Slider_Changed(object sender, EventArgs e)
        {
            var _sliderValue = ((Slider)sender).Value;
            var _songPoint = (_sliderValue * player.Duration) / 100;

            _numberLoops = _songPoint;
            if (!_fromTimer) {
                player.Seek(_songPoint);
            }
                
            _CronoTime.Text = Return_Time(_songPoint);
            _fromTimer = false;
        }

        //Alin B - TODO : refactor to a helper class.
        string Return_Time(double Value)
        {
            TimeSpan t = TimeSpan.FromSeconds(Value);
            //Alin B - return a string with format : e.g. 1h:01m:01s
            //string _returnTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
            //    t.Hours,
            //    t.Minutes,
            //    t.Seconds);

            //Alin B - TODO : maybe this should be refactored: 
            //If the song has 1h.1m.1s formtat is       : 1:01:01
            //And If the song has 0h.1m.1s formtat is   : 01:01
            string _returnTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                t.Hours,
                t.Minutes,
                t.Seconds);

            return _returnTime;
        }
    }
}
