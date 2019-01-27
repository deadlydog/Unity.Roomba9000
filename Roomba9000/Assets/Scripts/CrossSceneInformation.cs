using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	public static class CrossSceneInformation
	{
		public static GameOverReason GameOverReason;
		public static string GameOverReview;
	}

	public enum GameOverReason
	{
		NoPower = 0,
		TooDirty = 1
	}
}
