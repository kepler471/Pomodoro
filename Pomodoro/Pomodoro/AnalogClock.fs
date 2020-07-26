namespace Pomodoro

open System
open Eto.Forms
open Eto.Drawing

type AnalogClock() as x =
    inherit Drawable()
    let timer = new UITimer(Interval=1.)
    let sz = SizeF(100.f, 100.f)
    let r = sz.Width / 2.f

    do 
        timer.Elapsed.Add(fun _ -> x.Invalidate())
        timer.Start()
    override this.OnPaint e =
        let time = System.DateTime.Now

        let drawQuadrant (g:Graphics) =
          g.SaveTransform()
          for i = 1 to 12 do
            g.DrawLine(Pens.Black, 0.9f * r, 0.f, r, 0.f)
            g.RotateTransform(30.f)
          g.RestoreTransform()


        let drawHand (g:Graphics) (p:Pen) (len:single) (a:single) =
          g.SaveTransform()
          g.RotateTransform(a)
          g.DrawLine(p, -0.2f * r, 0.f, len * r, 0.f)
          g.RestoreTransform()

        let g = e.Graphics
        g.TranslateTransform(PointF(sz.Width / 2.f, sz.Height / 2.f))
        g.RotateTransform(-90.f)
        drawQuadrant g
        use p = new Pen(Colors.Black)
        p.Thickness <- 1.f
        drawHand g p 1.f (single(time.Second) * 6.f)
        p.Thickness <- 2.f
        drawHand g p 1.f (single(time.Minute) * 6.f)
        p.Thickness <- 2.f
        drawHand g p 0.7f (single(time.Hour) * 30.f)
        g.FillEllipse(Brushes.Red, -2.f, -2.f, 4.f, 4.f)
        

        