
# Chess Player movements and Highlight
I added all possible movements and highlighted the chess player.

### Piece Movement Highlighting:
Highlight possible moves for each type of chess piece:

* Pawn: Highlights forward moves, including the initial double-step move.
* Rook: Highlights vertical and horizontal moves.
* Knight: Highlights "L" shaped moves.
* Bishop: Highlights diagonal moves.
* Queen: Combines rook and bishop moves to highlight all possible directions.
* King: Highlights one-square moves in all directions.
### Object Pooling:
* Implemented an object pooling system to manage highlight objects efficiently.
* Created a pool of highlighted objects that can be reused, reducing the overhead of instantiating and destroying objects.
### Raycasting for Piece Selection:
* Utilized raycasting to detect mouse clicks on chess pieces.
* Implemented logic to highlight the selected piece's possible moves based on its type.
