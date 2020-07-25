// Learn more about F# at https://fsharp.org
// See the 'F# Tutorial' project for more help.

open System

let userTimer = 
    // Create event to wait on
    let event = new System.Threading.AutoResetEvent(false)

    // Create timer
    let timer = new System.Timers.Timer(2000.0)
    // Event handler
    timer.Elapsed.Add (fun _ -> event.Set() |> ignore)

    1


[<EntryPoint>]
let main argv =
    printfn "%A" argv
    0 // return an integer exit code
