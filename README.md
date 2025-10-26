# Monty Hall Problem Simulator

A C# console application that demonstrates the famous Monty Hall problem with interactive gameplay, Bayesian probability updates, and visual door representations.

## What is the Monty Hall Problem?

The Monty Hall problem is a probability puzzle based on the American television game show "Let's Make a Deal" and named after its original host, Monty Hall. The problem demonstrates how our intuition about probability can be misleading.

### The Setup
- You are given the choice of 3 doors (or more)
- Behind one door is a prize (car, money, etc.)
- Behind the other doors are goats (or nothing)
- You pick a door, but don't open it yet
- The host, who knows what's behind each door, opens one of the remaining doors, revealing a goat
- The host then asks: "Do you want to switch to the other unopened door or stay with your original choice?"

### The Counter-Intuitive Answer
**You should always switch!** Switching gives you a 2/3 chance of winning (with 3 doors), while staying gives you only a 1/3 chance.

## Why Switching is Better

### With 3 Doors:
- **Initial probability**: Each door has 1/3 chance
- **After host opens empty door**: 
  - Your door: Still 1/3 chance
  - Remaining door: 2/3 chance (gets all the probability from the opened door)

### With n Doors:
- **Initial probability**: Each door has 1/n chance
- **After host opens (n-2) empty doors**:
  - Your door: Still 1/n chance
  - Remaining door: (n-1)/n chance

## Visual Representation

### Door States
The simulator uses different visual styles to represent door states:

```
┌─────┐  ╔═════╗  ┌─┬─┬─┐  ┌─┬─┬─┐
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
└─────┘  ╚═════╝  └─┴─┴─┘  └─┴─┴─┘
Closed   Your     Opened   Prize
Door     Pick     Empty    Door
```

### Probability Display
Probabilities are shown underneath each door:

```
   Door 1      Door 2      Door 3   
   =======      =======      =======   
   ┌─────┐      ╔═════╗      ┌─┬─┬─┐
   │ ███ │      ║ ███ ║      │─ $ ─│
   │ ███ │      ║ ███ ║      │─ $ ─│
   │ ███ │      ║ ███ ║      │─ $ ─│
   │ ███ │      ║ ███ ║      │─ $ ─│
   │ ███ │      ║ ███ ║      │─ $ ─│
   │ ███ │      ║ ███ ║      │─ $ ─│
   └─────┘      ╚═════╝      └─┴─┴─┘
   0.0%         33.3%         66.7%
```

## Features

### Interactive Gameplay
- **Configurable doors**: Choose between 2-10 doors
- **Prize placement**: Random or manual selection
- **Two-phase gameplay**: Initial pick → Host opens doors → Final choice
- **Visual feedback**: See probabilities update in real-time

### Bayesian Probability Updates
- **Real-time calculations**: Probabilities update as doors are opened
- **Correct Monty Hall logic**: Demonstrates why switching is optimal
- **Mathematical accuracy**: Proper Bayesian inference implementation

### Visual Design
- **Multiple door styles**: Different frames for different states
- **Prize visualization**: Vertical $ symbols for prize doors
- **Probability display**: Centered underneath each door
- **Clear legend**: Explains all visual elements

## How to Run

### Prerequisites
- .NET 8.0 SDK or later
- Windows, macOS, or Linux

### Building and Running
```bash
# Clone the repository
git clone <repository-url>
cd Infer.NET-MontyHall

# Build the project
dotnet build

# Run the simulator
dotnet run
```

### Game Flow
1. **Setup**: Choose number of doors (2-10) and prize location
2. **Phase 1**: Pick your initial door
3. **Host Action**: Host opens empty doors, probabilities update
4. **Phase 2**: Choose to switch or stay with your original pick
5. **Result**: See if you won or lost!

## Example Game Session

```
Monty Hall Problem - 3 Doors
============================

   Door 1      Door 2      Door 3   
   =======      =======      =======   

   ┌─────┐      ┌─────┐      ╔═════╗
   │ ███ │      │ ███ │      ║ ███ ║
   │ ███ │      │ ███ │      ║ ███ ║
   │ ███ │      │ ███ │      ║ ███ ║
   │ ███ │      │ ███ │      ║ ███ ║
   │ ███ │      │ ███ │      ║ ███ ║
   │ ███ │      │ ███ │      ║ ███ ║
   └─────┘      └─────┘      ╚═════╝
   33.3%         33.3%         33.3%

Phase 1: Make your initial pick
Enter command: 3

You picked door 3!
Press any key to continue...

Monty Hall Problem - 3 Doors
============================

   Door 1      Door 2      Door 3   
   =======      =======      =======   

   ┌─┬─┬─┐      ┌─────┐      ╔═════╗
   │─   ─│      │ ███ │      ║ ███ ║
   │─   ─│      │ ███ │      ║ ███ ║
   │─   ─│      │ ███ │      ║ ███ ║
   │─   ─│      │ ███ │      ║ ███ ║
   │─   ─│      │ ███ │      ║ ███ ║
   │─   ─│      │ ███ │      ║ ███ ║
   └─┴─┴─┘      └─────┘      ╚═════╝
   0.0%         66.7%         33.3%

Phase 2: Switch to door 2 or stay with door 3?
Final Choice:
S: Switch to door 2
K: Keep door 3
Enter command: S

You switched to door 2!

Monty Hall Problem - 3 Doors
============================

   Door 1      Door 2      Door 3   
   =======      =======      =======   

   ┌─┬─┬─┐      ┌─┬─┬─┐      ╔═════╗
   │─   ─│      │─ $ ─│      ║ ███ ║
   │─   ─│      │─ $ ─│      ║ ███ ║
   │─   ─│      │─ $ ─│      ║ ███ ║
   │─   ─│      │─ $ ─│      ║ ███ ║
   │─   ─│      │─ $ ─│      ║ ███ ║
   │─   ─│      │─ $ ─│      ║ ███ ║
   └─┴─┴─┘      └─┴─┴─┘      ╚═════╝
   0.0%         100.0%        0.0%

Congratulations! You won!
The prize was behind door 2.
```

## Technical Implementation

### Architecture
- **Monty.cs**: Game logic and flow control
- **Doors.cs**: Door state management and visual rendering
- **Bayes.cs**: Bayesian probability calculations
- **Program.cs**: User interface and input handling

### Key Algorithms
- **Bayesian Updates**: Proper probability recalculation when doors are opened
- **Monty Hall Logic**: Correct implementation of host behavior
- **Visual Rendering**: ASCII art generation for different door states

## Educational Value

This simulator is perfect for:
- **Learning probability theory**: See Bayesian updates in action
- **Understanding the Monty Hall problem**: Visual demonstration of counter-intuitive probability
- **Programming education**: Clean C# code with good separation of concerns
- **Mathematical exploration**: Experiment with different numbers of doors

## Contributing

Contributions are welcome! Please feel free to submit issues, feature requests, or pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## References

- [Monty Hall Problem - Wikipedia](https://en.wikipedia.org/wiki/Monty_Hall_problem)
- [Bayes' Theorem - Wikipedia](https://en.wikipedia.org/wiki/Bayes%27_theorem)
- [Let's Make a Deal - Wikipedia](https://en.wikipedia.org/wiki/Let%27s_Make_a_Deal)
