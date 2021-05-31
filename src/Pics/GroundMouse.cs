namespace Pics {
    public class GroundMouse : Mouse
    {
        public GroundMouse(Coordinates coordinates, int speed, Direction direction) : base(Ground.Ground)
        {
            this.coordinates = coordinates;
            this.speed = speed;
            this.direction = direction;
            //
        }
    }
}