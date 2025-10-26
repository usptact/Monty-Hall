# Monty Hall Problem Simulator

A C# console application demonstrating the famous Monty Hall probability puzzle with interactive gameplay and Bayesian probability updates.

## The Problem

You're given 3+ doors. Behind one is a prize, others are empty. You pick a door, the host opens empty doors, then asks: "Switch or stay?"

**Answer: Always switch!** With 3 doors, switching gives you 2/3 chance vs 1/3 for staying.

## Visual Representation

```
┌─────┐  ╔═════╗  ┌─┬─┬─┐  ┌─┬─┬─┐
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
│ ███ │  ║ ███ ║  │─   ─│  │─ $ ─│
└─────┘  ╚═════╝  └─┴─┴─┘  └─┴─┴─┘
Closed   Your     Opened   Prize
Door     Pick     Empty    Door
```

**Legend:** `[█]` = Closed Door, `[ ]` = Opened Door, `[X]` = Your Pick, `[$]` = Prize!

## Features

- **2-10 doors**: Configurable game size
- **Real-time probabilities**: See Bayesian updates as doors open
- **Visual feedback**: Different door styles for each state
- **Interactive gameplay**: Two-phase pick → host opens → final choice

## Quick Start

```bash
# Build and run
dotnet build
dotnet run

# Game flow:
# 1. Choose doors (2-10) and prize location
# 2. Pick initial door
# 3. Host opens empty doors, probabilities update
# 4. Switch or stay?
# 5. See result!
```

## Example Session

```
Monty Hall Problem - 3 Doors
============================

   Door 1      Door 2      Door 3   
   =======      =======      =======   
   ┌─────┐      ┌─────┐      ╔═════╗
   │ ███ │      │ ███ │      ║ ███ ║
   │ ███ │      │ ███ │      ║ ███ ║
   └─────┘      └─────┘      ╚═════╝
   33.3%         33.3%         33.3%

Phase 1: Make your initial pick
Enter door number (1-3) or P for prize location, Q to quit: 3

You picked door 3!
Press any key to continue...

   ┌─┬─┬─┐      ┌─────┐      ╔═════╗
   │─   ─│      │ ███ │      ║ ███ ║
   │─   ─│      │ ███ │      ║ ███ ║
   └─┴─┴─┘      └─────┘      ╚═════╝
   0.0%         66.7%         33.3%

Phase 2: Switch to door 2 or stay with door 3?
S: Switch to door 2
K: Keep door 3
Enter command: S

You switched to door 2!
Congratulations! You won!
```

## Architecture

- **Program.cs**: User interface and game flow
- **Monty.cs**: Game logic and host behavior  
- **Doors.cs**: Door states and visual rendering
- **Bayes.cs**: Bayesian probability calculations

## Requirements

- .NET 8.0 SDK
- Windows/macOS/Linux

## License

MIT License - see [LICENSE](LICENSE) file.

## References

- [Monty Hall Problem](https://en.wikipedia.org/wiki/Monty_Hall_problem)
- [Bayes' Theorem](https://en.wikipedia.org/wiki/Bayes%27_theorem)