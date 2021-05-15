SetFactory("OpenCASCADE");
Point(1) = {-0.05, 0.05, 0, 0.001};
Point(2) = {-0.05, -0.05, 0, 0.001};
Point(3) = {0.05, -0.05, 0, 0.001};
Point(4) = {0.05, 0.05, 0, 0.001};
Line(1) = {1,2};
Line(2) = {2,3};
Line(3) = {3,4};
Line(4) = {4,1};
Line Loop(5)={1,2,3,4};
Plane Surface(6) = {5};
Show "*"
