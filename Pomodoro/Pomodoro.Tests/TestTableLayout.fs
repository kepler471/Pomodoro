namespace Pomodoro.Tests

open Pomodoro
open System
open Eto.Forms
open Eto.Drawing

type TableLayoutForm() as this =
    inherit Form()
    
    do
        this.ClientSize <- Size(500, 300)
        this.Title <- "Table Layout"

        let dropdown = new DropDown()


        // Timer controls
        // Might want to create command types for these
        let playPause = new Button(Text = "\u25B6/\u23F8 ")
        let stop = new Button(Text = "\u23F9")
        let skip = new Button(Text = "\u23ED")
        
        // Time adjustment sliders
        let workInterval = new Slider()
        let shortBreak = new Slider()
        let longBreak = new Slider()

        let lab1of1 = new Label(Text = "Set work interval")
        let lab1of2 = new Label(Text = "Set break interval")
        let lab1of3 = new Label(Text = "Set long break interval")
        lab1of1.TextAlignment <- TextAlignment.Center
        lab1of2.TextAlignment <- TextAlignment.Center
        lab1of3.TextAlignment <- TextAlignment.Center

        ["Item 1"; "Item 2"; "Item 3"]
        |> List.iter dropdown.Items.Add

        // The main layout mechanism for Eto.Forms is a TableLayout.
        // This is recommended to allow controls to keep their natural platform-specific size.
        // You can layout your controls declaratively using rows and columns as below, or add 
        // to the TableLayout.Rows and TableRow.Cell directly.
        let layout = 
            new TableLayout(
                Spacing = Size(5, 20) // space between each cell
                , Padding = Padding(20)
            )

        [
            TableRow(
                TableCell(playPause, true)
                , TableCell(stop, true)
                , TableCell(skip, true)
        
            )
            TableRow()
            TableRow(
                  TableCell(lab1of1)
                , TableCell(lab1of2)
                , TableCell(lab1of3)
            )
            // by default, the last row & column will get scaled. This adds a row at the end to 
            // take the extra space of the form.
            // otherwise, the above row will get scaled and stretch the TextBox/ComboBox/CheckBox
            // to fill the remaining height.
            TableRow(
                TableCell(workInterval)
                , TableCell(shortBreak)
                , TableCell(longBreak)
            )
            TableRow(ScaleHeight = true)
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

