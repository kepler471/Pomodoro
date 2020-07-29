namespace Pomodoro.Tests

open Pomodoro
open System
open Eto.Forms
open Eto.Drawing

type TableLayoutForm() as this =
    inherit Form()
    
    do
        base.ClientSize <- Size(500, 300)
        base.Title <- "Table Layout"

        // Timer controls
        // Might want to create command types for these
        let playPause = new Button(Text = "\u25B6/\u23F8 ")
        let stop = new Button(Text = "\u23F9")
        let skip = new Button(Text = "\u23ED")
        
        // Time adjustment sliders
        let workInterval = new Slider()
        let shortBreak = new Slider()
        let longBreak = new Slider()
        
        let sliderLabels = 
            [ "Set work interval"; "Set break interval"; "Set long break interval" ] 
            |> List.map (fun comment -> new Label(Text = comment))

        // Can this be done in the sliderLabels assignment?
        sliderLabels |> List.iter (fun label -> label.TextAlignment <- TextAlignment.Center)

        let layout = 
            new TableLayout(
                Spacing = Size(5, 20) // space between each cell
                , Padding = Padding(20)
            )
        
        // Define Table Structure
        [
            TableRow( 
                // true in tuples assures equal column widths
                [playPause, true; stop, true; skip, true ] |> List.map TableCell 
            )
            TableRow( 
                ScaleHeight = true 
            )
            TableRow( 
                sliderLabels |> List.map TableCell 
            )
            TableRow( 
                [ workInterval; shortBreak; longBreak ] |> List.map TableCell 
            )
            TableRow( 
                ScaleHeight = true 
            )
        ] |> List.iter layout.Rows.Add

        base.Content <- layout

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

        // Some notes:
        //  1. When scaling the width of a cell, it applies to all cells in the same column.
        //  2. When scaling the height of a row, it applies to the entire row.
        //  3. Scaling a row/column makes it share all remaining space with other scaled rows/columns.
        //  4. If a row/column is not scaled, it will be the size of the largest control in that row/column.

