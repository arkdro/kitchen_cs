namespace Pics {
    public class SnowMouse : Mouse
    {
        public SnowMouse(Coordinates coordinates, int speed, Direction direction) : base(Ground.Snow)
        {
            this.coordinates = coordinates;
            this.speed = speed;
            this.direction = direction;
        }
    }
}