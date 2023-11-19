namespace Ecs
{
    public struct Vector3D
    {
        public int x;
        public int y;
        public int z;

        public Vector3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3D(Vector3D source)
        {
            x = source.x;
            y = source.y;
            z = source.z;
        }
    }
}
