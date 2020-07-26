namespace Pomodoro.Desktop
module Program =

    open System
    open Pomodoro.Tests

    [<EntryPoint>]
    [<STAThread>]
    let Main(args) = 
        let app = new Eto.Forms.Application(Eto.Platform.Detect)
        app.Run(new MyForm())
        app.Run(new TestStackLayoutForm()) |> ignore
        0