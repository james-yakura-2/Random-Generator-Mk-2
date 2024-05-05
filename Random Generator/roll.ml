type die_result =
| Text of string                  (*A word or string*)
| Number of float                 (*A number*)
| Explosion of die_result * die   (*A die result, followed by a reroll*)
| Phrase of die list              (*A series of die rolls*)
| Pushable_Phrase of die list     (*A series of die rolls, the result of which is added to this die*)

type die = die_result list

exception Roll_error

let random_element lst =
let n=Random.int(Array.length lst) in
Array.get lst n

let rec roll_die (d:die)=
match random_element d with
| Text (str) -> (d, [Text (str)])
| Number (num) -> (d, [Number (num)])
| Explosion (res, exp) -> (d, [res] @ roll_die exp)
| Phrase (dice) -> (d, roll_all_dice dice)
| Pushable_Phrase (dice) -> 
  (match roll_all_dice dice with
  | res
  | _ )
| _ -> raise Roll_error

and roll_all_dice (dice:die list) =
match dice with
| h::t -> [roll_die h] @ roll_all_dice t
| [] -> []
| _ -> raise Roll_error

and parse_to_phrase (res:die_result list) =
| h::t -> 
  (match parse_to_phrase t with
  | Phrase(results)
  | Text(str)
  | Number(num)
  )
| []
| _