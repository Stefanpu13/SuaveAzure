// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open Suave.Writers
open Suave.Web
open System

let setCORSHeaders =
    setHeader "Access-Control-Allow-Origin" "*"     
    >=> setHeader "Access-Control-Allow-Headers" "content-type"     

let setCORS context = 
    context |> (
        setCORSHeaders
        >=> OK "CORS approved" )

let allowCors : WebPart =
    choose [
        OPTIONS >=> setCORS
        //GET >=> setCORS            
    ]

let baseUrl = path "/api"
[<EntryPoint>]
let main argv = 
    let webPart = 
        choose [            
           GET >=> baseUrl >=> path "/voting" >=> OK "Votes"
        ]  
    
    startWebServer 
        ({defaultConfig with 
            bindings = 
                [ HttpBinding.createSimple HTTP "127.0.0.1" 8085]}
        ) 
        webPart
    0 // return an integer exit code
