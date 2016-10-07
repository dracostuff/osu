﻿//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Audio.Sample;
using osu.Framework.Audio.Track;
using osu.Framework.GameModes;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Transformations;
using osu.Game.GameModes.Backgrounds;
using OpenTK.Graphics;

namespace osu.Game.GameModes.Menu
{
    class Intro : OsuGameMode
    {
        private OsuLogo logo;
        protected override BackgroundMode CreateBackground() => new BackgroundModeEmpty();

        public override void Load()
        {
            base.Load();

            Children = new Drawable[]
            {
                logo = new OsuLogo()
                {
                    Alpha = 0,
                    Additive = true,
                    Interactive = false,
                    Colour = Color4.DarkGray,
                    Ripple = false
                }
            };

            AudioSample welcome = Game.Audio.Sample.Get(@"welcome");
            welcome.Play();

            AudioTrack bgm = Game.Audio.Track.Get(@"circles");
            bgm.Looping = true;

            Game.Scheduler.Add(delegate
            {
                welcome.Play();
            }, true);


            Game.Scheduler.AddDelayed(delegate
            {
                bgm.Start();
            }, 600);

            Game.Scheduler.AddDelayed(delegate
            {
                Push(new MainMenu());
            }, 2900);

            logo.ScaleTo(0);

            logo.ScaleTo(1,5900, EasingTypes.OutQuint);
            logo.FadeIn(30000, EasingTypes.OutQuint);
        }

        protected override void OnSuspending(GameMode next)
        {
            Content.FadeOut(300);
            base.OnSuspending(next);
        }

        protected override void OnResuming(GameMode last)
        {
            //this is an exit
            Game.Scheduler.AddDelayed(delegate
            {
                Game.Exit();
            }, 300);

            base.OnResuming(last);
        }
    }
}
