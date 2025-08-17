using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CubeTower.Core
{
    public class CubesMovement 
    {
        private const float DefaultAnimDuration = .25f;
        private const float JumpDuration = .5f;
        private const float JumpPower = 1.5f;
        private const float SpeedDownDuration = .05f;
        private const int JumpCount = 1;
        
        public CubesMovement(Map map)
        {
            _map = map;
        }
        
        private readonly Map _map;
        
        public void MoveCube(CubeView cube, int speedDown = 0)
        {
            var target = new Vector3(cube.Data.PosX, _map.CalculateCubeHeight(cube), 0);
            
            float jumpDuration = DefaultAnimDuration + speedDown * SpeedDownDuration;
            
            cube.transform.DOJump(target, JumpPower, JumpCount, jumpDuration);
        }

        public void MoveToTrash(CubeView cube, Bounds b)
        {
            var pos = new Vector2(b.center.x, b.max.y);
            var minPos = new Vector2(b.center.x, b.min.y);
            
            cube.transform.DOJump(pos, JumpPower, JumpCount, JumpDuration);
            cube.transform.DOMove(minPos, DefaultAnimDuration).SetDelay(JumpDuration);
            cube.transform.DOScale(0, DefaultAnimDuration).SetDelay(JumpDuration).OnComplete(() => Destroy(cube));
        }

        public void MoveWrong(CubeView cube, Vector2 pos)
        {
            cube.transform.DOJump(pos, JumpPower, JumpCount, DefaultAnimDuration);
            cube.transform.DOScale(0, DefaultAnimDuration).SetDelay(DefaultAnimDuration).OnComplete(() => Destroy(cube));
        }

        public void FallCubeWithCallback(CubeView c, float groundPos, int speedDown)
        {
            float animDur = DefaultAnimDuration + SpeedDownDuration * speedDown;
            var pos = c.transform.position;
            pos.y = groundPos;
            c.transform.DOJump(pos, JumpPower, JumpCount, animDur);
            c.transform.DOScale(0, DefaultAnimDuration).SetDelay(speedDown * SpeedDownDuration).OnComplete(() => Destroy(c));
        }

        public void MoveFromGround(CubeView c, int speedDown = 0)
        {
            var pos = new Vector3(c.Data.PosX, _map.Ground);
            
            float animDur = DefaultAnimDuration + SpeedDownDuration * speedDown;

            c.transform.position = pos;
            
            c.transform.DOJump(new Vector3(c.Data.PosX, _map.CalculateCubeHeight(c), 0), JumpPower, JumpCount, animDur);
        }

        private void Destroy(CubeView c) => Object.Destroy(c.gameObject);
    }
}