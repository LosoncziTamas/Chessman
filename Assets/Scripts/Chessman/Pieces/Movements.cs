using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessman.Pieces
{
    public static class Movements
    { 
        [Flags]
        public enum MoveType
        {
            None = 0,
            Pawn = 1 << 0,
            Knight = 1 << 1,
            Bishop = 1 << 2,
            Rook = 1 << 3,
            King = 1 << 4,
        }

        public static IEnumerable<Vector2Int> GetMoves(Vector2Int position, MoveType moveType, TileContainer container)
        {
            var result = new List<Vector2Int>();
            
            if (moveType.HasFlag(MoveType.Knight))
            {
                result.AddRange(GetKnightMoves(position));
            }
            if (moveType.HasFlag(MoveType.Bishop))
            {
                result.AddRange(GetBishopMoves(position, container));
            }
            if (moveType.HasFlag(MoveType.Rook))
            {
                result.AddRange(GetRookMoves(position, container));
            }
            
            return result;
        }

        private static IEnumerable<Vector2Int> GetKnightMoves(Vector2Int position)
        {
            var forwardLeft = new Vector2Int(position.x - 1, position.y + 2);
            var forwardLeftSmall = new Vector2Int(position.x - 2, position.y + 1);
            var forwardRight = new Vector2Int(position.x + 1, position.y + 2);
            var forwardRightSmall = new Vector2Int(position.x + 2, position.y + 1);
            
            var backwardLeft = new Vector2Int(position.x - 1, position.y - 2);
            var backwardLeftSmall = new Vector2Int(position.x - 2, position.y - 1);
            var backwardRight = new Vector2Int(position.x + 1, position.y - 2);
            var backwardRightSmall = new Vector2Int(position.x + 2, position.y - 1);

            return new List<Vector2Int>
            {
                forwardLeft,
                forwardLeftSmall,
                forwardRight,
                forwardRightSmall,
                backwardLeft,
                backwardLeftSmall,
                backwardRight,
                backwardRightSmall
            };
        }
        
        private static IEnumerable<Vector2Int> GetKingMoves(Vector2Int position)
        {
            var left = new Vector2Int(position.x - 1, position.y);
            var right = new Vector2Int(position.x + 1, position.y);
            var forward = new Vector2Int(position.x, position.y + 1);
            var backward = new Vector2Int(position.x, position.y - 1);
            var leftForward = new Vector2Int(position.x - 1, position.y + 1);
            var leftBackward = new Vector2Int(position.x - 1, position.y - 1);
            var rightBackward = new Vector2Int(position.x + 1, position.y - 1);
            var rightForward = new Vector2Int(position.x + 1, position.y + 1);

            return new List<Vector2Int>
            {
                left,
                right,
                forward,
                backward,
                leftForward,
                leftBackward,
                rightBackward,
                rightForward,
            };
        }

        private static IEnumerable<Vector2Int> GetBishopMoves(Vector2Int position, TileContainer tileContainer)
        {
            var result = new List<Vector2Int>();
            
            // forward right
            for (int x = position.x + 1, y = position.y + 1; x < TileContainer.BoardDimensionX && y < TileContainer.BoardDimensionY; x++, y++)
            {
                var pos = new Vector2Int(x, y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }
            }
            
            // forward left
            for (int x = position.x - 1, y = position.y + 1; x >= 0 && y < TileContainer.BoardDimensionY; x--, y++)
            {
                var pos = new Vector2Int(x, y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }            
            }
            
            // backward right
            for (int x = position.x + 1, y = position.y - 1; x < TileContainer.BoardDimensionX && y >= 0; x++, y--)
            {
                var pos = new Vector2Int(x, y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }            
            }
            
            // backward left
            for (int x = position.x - 1, y = position.y - 1; x >= 0 && y >= 0; x--, y--)
            {
                var pos = new Vector2Int(x, y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }            
            }

            return result;
        }

        private static IEnumerable<Vector2Int> GetRookMoves(Vector2Int position, TileContainer tileContainer)
        {
            var result = new List<Vector2Int>();

             // forward
            for (var y = position.y + 1; y < TileContainer.BoardDimensionY; y++)
            {
                var pos = new Vector2Int(position.x, y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }   
            }

            // backward
            for (var y = position.y - 1; y >= 0; y--)
            {
                var pos = new Vector2Int(position.x, y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }   
            }
            
            // left
            for (var x = position.x - 1; x >= 0; x--)
            {
                var pos = new Vector2Int(x, position.y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }   
            }
            
            // right
            for (var x = position.x + 1; x < TileContainer.BoardDimensionX; x++)
            {
                var pos = new Vector2Int(x, position.y);
                result.Add(pos);
                if (tileContainer.GetTile(pos).HasPiece)
                {
                    break;
                }   
            }
            
            return result;
        }
    }
}