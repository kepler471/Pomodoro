namespace Pomodoro.Tests
module Program =

    open System
    open Pomodoro.Tests

    [<EntryPoint>]
    [<STAThread>]
    let Main(args) = 
        let app = new Eto.Forms.Application(Eto.Platform.Detect)
        app.Run(new TableLayoutForm())
        0