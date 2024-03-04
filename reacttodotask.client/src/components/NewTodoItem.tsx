import React, { useState } from 'react';
import { format } from 'date-fns';
import remove from '../assets/delete.svg';
import { TodoItem } from "../models/TodoItem";
import './styles.css';

interface INewTodoItemComponentProps {
    todoItem: TodoItem;
    onCreate: (todoItem: TodoItem) => void;
}

export const NewTodoItemComponent: React.FC<INewTodoItemComponentProps> = ({
    todoItem,
    onCreate
}) => {
   
    const [todoInput, setTodoInput] = useState('');
    const [todoDeadlineDate, setTodoDeadlineDate] = useState('');
    let inputClass = "new-todo-input";
    let buttonDisable = false;
    let inputValid = true;
    let dateValid = true;
    let errorMessage = "";

    if (todoInput != null && todoInput.length < 10) {
        inputClass += " inputInvalid";        
        inputValid = false;
        if (todoInput.length>0)
        errorMessage = "The task must be longer than 10 characters";

    }
    if (todoDeadlineDate != null && todoDeadlineDate.length < 10) {       
        dateValid = false;
        if (inputValid == true)
        errorMessage = "Please select the deadline date";
    }
    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTodoInput(event.target.value);   
    };
    const handleDateChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTodoDeadlineDate(event.target.value);
       
    };
    if (!dateValid || !inputValid) {
        buttonDisable = true;
    }

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        
        console.log('Name:', todoInput);
        console.log('Selected Date:', todoDeadlineDate);

        todoItem.title = todoInput;
        todoItem.deadlineDate = new Date(todoDeadlineDate);

        onCreate(todoItem);
        setTodoInput('');
        setTodoDeadlineDate('');

    };
   
    /*onChange={handleNameChange}
    onChange={(e) => setTodoDeadlineDate(e.target.value)}
    onChange={(e) => setTodoInput(e.target.value)} 
    */ 
    return (
        
        <form onSubmit={handleSubmit}>
            <div className="new-todo-div">
             <div className="new-todo-inputs">
                <input
                    className={inputClass}
                    type="text"
                    placeholder="new todo item..."
                    value={todoInput}
                    onChange={handleInputChange}                    
                />
                <input
                    className="new-todo-date"
                    type="date"
                    value={todoDeadlineDate}
                    onChange={handleDateChange} 
                   
                />
                </div>
                <button type="submit" disabled={buttonDisable}>Add</button>
            </div>
            <div>
                <p className="errorMessage">{errorMessage}</p>
            </div>
        </form>
    );
};
