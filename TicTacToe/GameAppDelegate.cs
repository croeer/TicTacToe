﻿using System;
using CocosSharp;


namespace TicTacToe
{
	public class GameAppDelegate : CCApplicationDelegate
	{
		public override void ApplicationDidFinishLaunching (CCApplication application, CCWindow mainWindow)
		{
			application.PreferMultiSampling = false;
			application.ContentRootDirectory = "Content";
			application.ContentSearchPaths.Add ("animations");
			application.ContentSearchPaths.Add ("fonts");
			application.ContentSearchPaths.Add ("sounds");

			CCSize windowSize = mainWindow.WindowSizeInPixels;

			float desiredWidth = 600.0f;
			float desiredHeight = 600.0f;
            
			// This will set the world bounds to be (0,0, w, h)
			// CCSceneResolutionPolicy.ShowAll will ensure that the aspect ratio is preserved
			CCScene.SetDefaultDesignResolution (desiredWidth, desiredHeight, CCSceneResolutionPolicy.ShowAll);
            
			// Determine whether to use the high or low def versions of our images
			// Make sure the default texel to content size ratio is set correctly
			// Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
			if (desiredWidth < windowSize.Width) {
				application.ContentSearchPaths.Add ("images/hd");
				CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
			} else {
				application.ContentSearchPaths.Add ("images/ld");
				CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
			}
		
			mainWindow.RunWithScene (GameLayer.GameScene(mainWindow));
		}

		public override void ApplicationDidEnterBackground (CCApplication application)
		{
			application.Paused = true;
		}

		public override void ApplicationWillEnterForeground (CCApplication application)
		{
			application.Paused = false;
		}
	}
}
