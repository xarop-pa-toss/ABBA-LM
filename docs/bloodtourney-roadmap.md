# BloodTourney Library Roadmap
BloodTourney is a library for tournament creation and management for Blood Bowl. It handles tournamnet organization, team registration, validation of tournament rules, the use of base official rulesets or the adding of custom rulesets via code or JSON. It also handles all the logic for tournament play, including the format, team rankings, points system, etc

## 1. Custom Ruleset Creation
- Allow users to define custom rulesets programmatically or via JSON configuration.
- Implement validation to ensure custom rulesets are logically consistent.

## 2. Ruleset Validation
- Expand validation logic to cover:
  - Victory points and tie-breaker configurations.
  - Logical consistency in tier parameters.
- Provide detailed error messages for invalid rulesets.

## 3. Tournament Object
- Create a `Tournament` class to encapsulate:
  - Teams
  - Rounds
  - Rankings
  - Rulesets
- Add methods for managing tournament state:
  - `AddTeam`
  - `RecordMatchResult`
  - `UpdateRankings`

## 4. Team and Match Management
- Define a `Team` class with properties like `Name`, `Coach`, and `Roster`.
- Define a `Match` class to represent games, with properties like `TeamA`, `TeamB`, `ScoreA`, and `ScoreB`.
- Implement methods to schedule matches and track results.

## 5. Ladder and Ranking System
- Develop a ranking system based on match results, with support for tie-breakers.
- Add a `Ladder` class to dynamically manage rankings and positions.

## 6. Unit Tests
- Expand test coverage to include:
  - Ruleset validation.
  - Tournament creation and state management.
  - Team and match operations.
- Ensure all core functionality is reliable and regression-free.

## 7. Serialization
- Add support for saving and loading tournaments via JSON for persistence.

## 8. Refactoring
- Modularize the codebase by splitting large classes into smaller, focused components:
  - `RulesetManager`
  - `ValidationService`
- Improve readability and maintainability with clear documentation and consistent structure.

---