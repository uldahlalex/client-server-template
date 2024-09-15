# React component exercises (These are exercises for day 2 of the crash course)

### Intro
These exercises try to appeal to a more DIY-ish style of learning, where you try to solve small tasks on your own. 
If you require more support / walkthrough style exercises, I recommend picking the React Quick-Start guide instead: https://react.dev/learn

I recommend watching my video about React components if you don't know anything yet. (See my YouTube Channel: https://www.youtube.com/@alexdevden/videos)

I have made this template to give you starting point with recommended dependencies, Typescript configuration and linting.
There are some files you don't need - I recommend simply ignoring these - you can just make the exercises inside the exercises.tsx file.

You can start the app with **npm install** and **npm run dev** and navigating to localhost port 4200.

## Exercises in natural chronological order 
- Present the value of a variable inside a component.
- Conditionally render the value based on a boolean expression.
- Put a component inside another component.
- Present some component 3 times of types without duplicating the code (loop / iterate)
- Make a button with the prop "onClick" that logs "Button clicked" to the console.
- Make an "input" HTML element that logs the value of the input to the console when the user types.
- Use the useState hook to make a counter that increments by 1 when a button is clicked.
- Use the "input" HTML element to change the value of a variable presented in the component.
- Make a button and an "input" HTML element. The button has to be "disabled" when the input is empty.
- Iterate over a list of strings and present them in the UI as "div" elements. When you click one of the strings, it should remove it.
- The final boss: Implement full "CRUD" functionality for a list of strings.
  - Create a new string from the input field and add it to the list presented in the UI. The button should be disabled if the strings are shorter than 3 characters.
  - Update a string using a button and input field for each element
  - Delete a string using a button for each element
- Tier 2 final boss: Instead of using strings, make a model using an interface. The string should be a property of the interface
