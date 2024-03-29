# Gravity Simulation
Welcome to my gravity simulator! This application offers a 3D experience, allowing you to simulate and visualize Newton's law of universal gravitation. Observe the captivating interactions between planets and stars as they move and interact in a 3D environment.

# Install
Only Windows version is available. To install the simulator, download [this instaler](Installers/GravitySimulationSetup.exe) (download button is in the right upper corner). I'm aware that some antivirus programs may flag the installer as potentially harmful, but I can assure you that it is entirely safe and free from any viruses. If you encounter any issues during installation, you may need to persuade your operating system that the app is safe in order to proceed with the installation.

# Purpose and Background:
This gravity simulator was created as a result of my college course assignment. The initial task was to write a gravity simulation using Euler integration. However, the simulation produced highly unstable results, failing to preserve the critical system properties like total energy and total momentum. Determined to find a more accurate and stable method for simulating gravity, I conducted further research on my own during summer holidays. Out of curiosity.

The decision to visualize the simulation was inspired by the desire to showcase the results of my work to others. I aimed to create an intuitive and visually pleasing experience, allowing everyone, regardless of their technical background, to witness the beauty and intricacies of laws of motion in action.

By presenting the simulation in a user-friendly and interactive manner, I sought to foster appreciation for the marvels of the cosmos beyond the realm of hard-to-understand lines of code.

Utilizing the Unity engine, the program's development grew rapidly, evolving into a nice simulation tool.

# Features:
* 3D Simulation: Experience a fully 3D simulation of Newton's law of universal gravitation and laws of motion, providing a visually engaging environment.

* Verlet Velocity Method: The simulation uses the Verlet Velocity method to calculate and simulate the bodies' movement. This method is easy to implement and works exceptionally well for Newtonian laws of motion.

* Planet and Star Objects: Observe two distinct types of celestial objects - planets and stars - each with its own characteristics and behaviors.

* Trail Visualization: Witness the movement of celestial bodies through visual trails, providing insight into their past movements.

* Trail Prediction: The application is equipped with prediction system that can show you bodies' trajectories in the future! This way you can pin down interresting moments and interactions.

* Collisions: The simulation incorporates a primitive collision system, allowing celestial bodies to merge while conserving momentum, but not, unfortunatelly, total energy. This approach strikes a balance between accuracy and stability, as it prevents bodies that come into close proximity from gaining unrealistic amounts of kinetic energy. By preserving momentum during collisions, the simulation aims to provide a more realistic simulation, while keeping it computationally feasible.

* Vector Toggle: Customize your view by toggling on/off bodies' vectors, including velocities, momentums, and forces, giving you a comprehensive understanding of their interactions.

* Time Control: Take control of the simulation speed with the ability to accelerate, decelerate, or pause the simulation entirely.

* UI Statistics: Stay informed throughout the simulation with the UI displaying various statistics, allowing you to track the dynamics of the system.

* Scene Switching: Explore a collection of premade scenes, each offering unique setups and scenarios for the simulation.

# Screenshots
## Simulation features
Trajectories

![bodies with its' trajectiories](./Documentation/trajectories.png)

Simulation panel

![simulation statistics panel](./Documentation/statistics.png)

Trajectories prediction

![simulation prediction feature](./Documentation/prediction.png)

Pure chaos

![almighty chaos](./Documentation/chaos.png)

An example of stable 3-body system

![stable lemniscate trajectories](./Documentation/lemniscate.png)

## Vectors
Force vectors

![force vectors](./Documentation/vectors/force.png)

Speed vectors

![speed vectors](./Documentation/vectors/speed.png)

Momentum vectors

![momentum vectors](./Documentation/vectors/momentum.png)

All vectors at once

![all vectors at once](./Documentation/vectors/all_vectors.png)

# Controls:
* WASD: Move the camera to explore the 3D environment.

* Shift + WASD: Accelerate the camera movement speed for faster navigation.

* Mouse scroll: Move the camera forward or backward along the scene.

* Click on a celestial body: follow the body

* P: Predict bodies' movement and observe their future positions.

* Up/Down Arrows: Adjust the simulation speed, making it faster or slower.

* 1-9: Set the simulation speed to respective numbers for precise control.

* Tilde (~): Stop the simulation, pausing all motion.

* Numpad 1, 2, 3: Control force vectors - 1: Increase length, 2: Toggle visibility, 3: Decrease length.

* Numpad 4, 5, 6: Control momentum vectors - 4: Increase length, 5: Toggle visibility, 6: Decrease length.

* Numpad 7, 8, 9: Control velocity vectors - 7: Increase length, 8: Toggle visibility, 9: Decrease length.

(Note: "Change length" refers to multiplying vector length by a factor, adjustable by the user)

* Left/Right Arrows: Navigate between previous and next scenes for a diverse simulation experience.

* Escape: Quit the program

#
Explore the fascinating world of Newton's law of universal gravitation, and enjoy the power to observe celestial mechanics firsthand. Have an extraordinary time discovering the wonders of the cosmos!
