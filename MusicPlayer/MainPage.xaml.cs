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
            player.Play();
        }

        void Pause_Button_Clicked(object sender, EventArgs e)
        {
            //Pause my default song.
            player.Pause();
        }
    }
}
