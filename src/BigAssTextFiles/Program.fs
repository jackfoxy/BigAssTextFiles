namespace BigAssTextFiles

open System.IO

module console1 =
    
    let ByteToHex bytes = 
        bytes 
        |> Array.map (fun (x : byte) -> System.String.Format("{0:X2}", x))
        |> String.concat System.String.Empty

    [<EntryPoint>]
    let main argv = 
        //if argv.[0] has $ in first character accessing directly either of next 2 lines throws in release, not in debug
        // go figure
        let data = argv.[0]

        printfn "input length %i %s" data.Length  data
        let data = System.Text.Encoding.ASCII.GetBytes(data)
        
        let sha = new System.Security.Cryptography.SHA1CryptoServiceProvider()
        let hash = sha.ComputeHash data
        let targetHash = ByteToHex hash

        printfn "the hash is %s" targetHash

        use file = new StreamReader("E:\BigData\pwned-passwords-1.0.txt")

        let rec loop (lineCount : int) =

            match file.ReadLine() with
            //| null ->
            //    printfn "line count is %s" <| System.String.Format("{0:N0}", lineCount)

            //| _ ->
            //    if (lineCount % 10000000) = 0 then
            //        printfn "%i0M" (lineCount / 10000000)
            //    else
            //        ()
            //    loop (lineCount + 1)

            //| null ->
            //    printfn "line count is %s" <| System.String.Format("{0:N0}", lineCount)

            //| x ->
            //    if lineCount = 100 then
            //        ()
            //    else
            //        printfn "%s" x
            //        loop (lineCount + 1) 0

            | null ->
                printfn "line count is %s" <| System.String.Format("{0:N0}", lineCount)

            | x ->
                if (lineCount % 10000000) = 0 then
                    printfn "%i0M" (lineCount / 10000000)
                else
                    ()

                if x = targetHash then
                    printfn "%s" x
                    printfn "match on line %s" <| System.String.Format("{0:N0}", (lineCount + 1))
                else
                    loop (lineCount + 1) 

        loop 0

        0
