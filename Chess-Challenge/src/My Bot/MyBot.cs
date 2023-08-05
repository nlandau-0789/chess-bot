using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        float Evaluate(Board board)
        {
            int dist(Square sq1, Square sq2) { return (sq1.Rank - sq2.Rank) * (sq1.Rank - sq2.Rank) + (sq1.File - sq2.File) * (sq1.File - sq2.File); }
            PieceList[] pieceLists = board.GetAllPieceLists();
            int colorMultiplier = board.IsWhiteToMove ? 1 : -1;
            float sum = 0;
            // var value = new Dictionary<Piece, float>();

            // white pawns
            foreach (Piece piece in pieceLists[0])
            {
                // value.Add(piece, piece.Square.Rank / 2);
                sum += piece.Square.Rank / 2;
            }

            // black pawns
            foreach (Piece piece in pieceLists[6])
            {
                // value.Add(piece, 4 - piece.Square.Rank / 2);
                sum -= 4 - piece.Square.Rank / 2;
            }

            // white knights
            foreach (Piece piece in pieceLists[1])
            {
                // value.Add(piece, 8 / dist(piece.Square, board.GetKingSquare(false)) + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetKnightAttacks(piece.Square)));
                sum += 8 / dist(piece.Square, board.GetKingSquare(false)) + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetKnightAttacks(piece.Square));
            }

            // black knights
            foreach (Piece piece in pieceLists[7])
            {
                // value.Add(piece, 8 / dist(piece.Square, board.GetKingSquare(true)) + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetKnightAttacks(piece.Square)));
                sum -= 8 / dist(piece.Square, board.GetKingSquare(true)) + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetKnightAttacks(piece.Square));
            }

            // white bishops
            foreach (Piece piece in pieceLists[2])
            {
                // value.Add(piece, dist(piece.Square, board.GetKingSquare(false)) / 4 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board)));
                sum += dist(piece.Square, board.GetKingSquare(false)) / 4 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board));
            }

            // black bishops
            foreach (Piece piece in pieceLists[8])
            {
                // value.Add(piece, dist(piece.Square, board.GetKingSquare(true)) / 4 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board)));
                sum -= dist(piece.Square, board.GetKingSquare(true)) / 4 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board));
            }

            // white rooks
            foreach (Piece piece in pieceLists[3])
            {
                // value.Add(piece, piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board)));
                sum += piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board));
            }

            // black rooks
            foreach (Piece piece in pieceLists[9])
            {
                // value.Add(piece, 4 - piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board)));
                sum -= 4 - piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board));
            }

            // white queen
            foreach (Piece piece in pieceLists[4])
            {
                // value.Add(piece, 16 / dist(piece.Square, board.GetKingSquare(false)) + piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board)));
                sum += 16 / dist(piece.Square, board.GetKingSquare(false)) + piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board));
            }

            // black queen
            foreach (Piece piece in pieceLists[10])
            {
                // value.Add(piece, 16 / dist(piece.Square, board.GetKingSquare(true)) + 4 - piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board)));
                sum -= 16 / dist(piece.Square, board.GetKingSquare(true)) + 4 - piece.Square.Rank / 2 + BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetSliderAttacks(piece.PieceType, piece.Square, board));
            }

            // white king
            foreach (Piece piece in pieceLists[5])
            {
                // value.Add(piece, dist(piece.Square, new Square("d5")) + (board.IsInCheck() && board.IsWhiteToMove ? 10 : 0) + (board.IsInCheckmate() && board.IsWhiteToMove ? float.NegativeInfinity : 0));
                sum += dist(piece.Square, new Square("d5")) + (board.IsInCheck() && board.IsWhiteToMove ? 10 : 0) + (board.IsInCheckmate() && board.IsWhiteToMove ? float.NegativeInfinity : 0);
            }

            // black king
            foreach (Piece piece in pieceLists[11])
            {
                // value.Add(piece, dist(piece.Square, new Square("e4")) + (board.IsInCheck() && !board.IsWhiteToMove ? 10 : 0) + (board.IsInCheckmate() && !board.IsWhiteToMove ? float.NegativeInfinity : 0));
                sum -= dist(piece.Square, new Square("e4")) + (board.IsInCheck() && !board.IsWhiteToMove ? 10 : 0) + (board.IsInCheckmate() && !board.IsWhiteToMove ? float.NegativeInfinity : 0);
            }

            return sum * colorMultiplier;
        }

        // (Move move, float value) findMove(Board board, int maxDepth)
        // {
        //     Move[] moves = board.GetLegalMoves();
        //     if (maxDepth == 0){
        //         return (moves[0], evaluate(board));
        //     }
        //     (Move move, float value)[] bestMoves = {(moves[0], float.NegativeInfinity), (moves[0], float.NegativeInfinity), (moves[0], float.NegativeInfinity)};
        //     foreach (Move move in moves){
        //         board.MakeMove(move);
        //         if (findMove(board, maxDepth-1).value > bestMoves[2].value){
        //             bestMoves[2] = (move, findMove(board, maxDepth-1).value);
        //             bestMoves = bestMoves.OrderByDescending(tuple => tuple.value).ToArray();
        //         }
        //         board.UndoMove(move);
        //     }
        //     Console.WriteLine(bestMoves[0].move);
        //     Console.WriteLine(bestMoves[0].value);
        //     return bestMoves[0];
        // }
        (Move move, float value) FindMove(Board board, int depth, bool maximizingPlayer)
        {
            Move[] moves = board.GetLegalMoves();

            // Base case: if we reach the maximum depth or no legal moves available, return the evaluation of the current board position
            if (depth == 0 || moves.Length == 0)
            {
                return (Move.NullMove, Evaluate(board));
            }

            Move bestMove = Move.NullMove;
            float bestValue = maximizingPlayer ? float.NegativeInfinity : float.PositiveInfinity;

            foreach (Move move in moves)
            {
                board.MakeMove(move);

                // Recursively call FindMove with reduced depth and switch the maximizing player for the next level
                var (_, value) = FindMove(board, depth - 1, !maximizingPlayer);

                // Update the best move and best value based on the player's turn
                if (maximizingPlayer && value > bestValue)
                {
                    bestValue = value;
                    bestMove = move;
                }
                else if (!maximizingPlayer && value < bestValue)
                {
                    bestValue = value;
                    bestMove = move;
                }

                board.UndoMove(move);
            }
            // Console.WriteLine(bestMove);
            // Console.WriteLine(bestValue);

            return (bestMove, bestValue);
        }
        return FindMove(board, 4, !board.IsWhiteToMove).move;
    }
}