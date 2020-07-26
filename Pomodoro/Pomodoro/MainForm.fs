namespace Pomodoro

open System
open Eto.Forms
open Eto.Drawing

type MainForm () as this =
    inherit Form()
    do
        //let icon = new TrayIndicator()

        base.Title <- "Pomodoro Timer"
        base.ClientSize <- new Size(300, 350)

        let layout = new StackLayout(
                            Orientation = Orientation.Vertical,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            Spacing = 5,
                            Padding = new Padding(10)
                            )

        layout.Items.Add(new StackLayoutItem(new Label(Text = "Set work interval")))
        layout.Items.Add(new StackLayoutItem(new Slider()))
        layout.Items.Add(new StackLayoutItem(new Label(Text = "Set break interval")))
        layout.Items.Add(new StackLayoutItem(new Slider()))
        layout.Items.Add(new StackLayoutItem(new Label(Text = "Set long break interval")))
        layout.Items.Add(new StackLayoutItem(new Slider()))

        layout.Items.Add(new StackLayoutItem(new AnalogClock()))
        
        base.Content <- layout;

        let startPause = new Command(ToolBarText = "\u25B6")
        let stop = new Command(ToolBarText = "\u23F9")
        let reset = new Command(ToolBarText = "\u23EE")
        let skip = new Command(ToolBarText = "\u23ED")
        startPause.Executed.Add(fun e -> MessageBox.Show(this, "Pause/Play Timer") |> ignore)
        stop.Executed.Add(fun e -> MessageBox.Show(this, "Stop timer") |> ignore)
        reset.Executed.Add(fun e -> MessageBox.Show(this, "Restart the current session") |> ignore)
        skip.Executed.Add(fun e -> MessageBox.Show(this, "Skip to end of current session") |> ignore)
        let pomControls = [ startPause; stop; reset; skip ]

        let quitCommand = new Command(MenuText = "Quit")
        quitCommand.Shortcut <- Application.Instance.CommonModifier ||| Keys.Q
        quitCommand.Executed.Add(fun e -> Application.Instance.Quit())

        let aboutCommand = new Command(MenuText = "About...")
        aboutCommand.Shortcut <- Application.Instance.CommonModifier ||| Keys.I
        aboutCommand.Executed.Add(fun e ->
            let dlg = new AboutDialog()
            dlg.ShowDialog(this) |> ignore
            )

        base.Menu <- new MenuBar()
        let fileItem = new ButtonMenuItem(Text = "&File")

        base.Menu.ApplicationItems.Add(new ButtonMenuItem(Text = "&Preferences..."))
        base.Menu.QuitItem <- quitCommand.CreateMenuItem()
        base.Menu.AboutItem <- aboutCommand.CreateMenuItem()

        base.ToolBar <- new ToolBar()
        [ startPause; stop; reset; skip ]
        |> List.iter base.ToolBar.Items.Add

        //this.ToolBar.Items.Add(new SeparatorToolItem())
