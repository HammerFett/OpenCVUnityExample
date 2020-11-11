# OpenCVUnityExample
Short Test of Unity with OpenCV 

Just open up as a Unity Project and hit play.

If you move your head upwards infront of the Camera the ball will jump.

Hope this helps as a starting point.

I followed this tutorial:
https://thomasmountainborn.com/2016/09/11/unity-and-opencv-part-one-install/

and created a .dll with c++ which starts a camera feed and detects a face which is then send to Unity.
Unity is fetching the data and depending on the height of the detected face makes a ball jump.

This ball gets a constant velocity on its x-Axis while z is frozen.

The rest is just some minor things like respawning when falling of the Parkour
