using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DMF
{
    class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        private Vector2 centre;
        private Viewport viewport;

        private float zoom = 1;

        public float X
        {
            get { return centre.X; }
            set { centre.X = value; }
        }

        public float Y
        {
            get { return centre.Y; }
            set { centre.Y = value; }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.5f)
                    zoom = 0.5f;
                else if (zoom > 0.7f)
                    zoom = 0.7f;
            }
        }

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 positon)
        {
            centre = new Vector2(positon.X, positon.Y);

            transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) *
                Matrix.CreateScale(new Vector3(Zoom, zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
    }
}
