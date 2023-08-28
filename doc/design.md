# Design of the Game

## Plate Interface

A plate can holds at most $maxObject KitchenObjects on it. 

The player can hold the plate when interact with the plate counter.

When the player is holding the plate, the player can place other Kitchen ojbect onto the plate with interact.

The plate can only be placed in clear counter.

When the plate is placed in clear counter and the player is not holding anything, the player can pick up the plate by interacting with the clear counter.

When the plate is placed in clear counter and the player is holding a suitable kithen object, the player can put the object into the plate, which is held by the clear counter by interacting with the clear counter.

## Useful C\# Features

- keyword `in` 
- List, method `Contains()`
