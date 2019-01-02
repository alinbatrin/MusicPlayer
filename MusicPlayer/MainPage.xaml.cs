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
        public MainPage()
		{
			InitializeComponent();
            //Player load my default song.
            player.Load("M1.mp3");
        }

        
        void Play_Button_Clicked(object sender, EventArgs e)
        {
            //Play my default song.
            //**Specificatii cand dau play stiu cat dureaza melodia.
            //Cand 
            player.Play();
        }

        void Pause_Button_Clicked(object sender, EventArgs e)
        {
            //Pause my default song.
            player.Pause();
        }

        void Stop_Button_Clicked(object sender, EventArgs e)
        {
            //Pause my default song.
            player.Stop();
        }

        void Slider_Changed(object sender, EventArgs e)
        {
            var _sliderValue = ((Slider)sender).Value;
            var _songLength = player.Duration;

            player.Seek((_sliderValue * _songLength) / 100);
        }
    }
}
