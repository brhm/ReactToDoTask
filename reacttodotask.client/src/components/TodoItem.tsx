import React, { useState } from 'react';
import { format } from 'date-fns';
import remove from '../assets/delete.svg';
import { TodoItem } from "../models/TodoItem";
import './styles.css';
import { todo } from 'node:test';

interface ITodoItemComponentProps {
    todoItem: TodoItem;
    onDelete: (todoItem: TodoItem) => void;
    onMarkAsCompleted: (todoItem: TodoItem) => void;
}

export const TodoItemComponent: React.FC<ITodoItemComponentProps> = ({
    todoItem,
    onDelete,
    onMarkAsCompleted
}) => {

    console.log(todoItem);
    
    var overDue = "todo-row";
    if (todoItem.completed == false && Date.parse(todoItem.deadlineDate.toString()) < Date.now()) {
        overDue = "todo-row overDueClass";
    }

    const [completed, setCompleted] = useState(todoItem.completed);

    const handleCompleted = () => {
        onMarkAsCompleted({ ...todoItem, completed: !completed });
        setCompleted(!completed);
    };

    const handleDelete = () => {
        onDelete(todoItem);
    };

    return (
        <div className={overDue} >
          <input
                className="completed-checkbox"
                type="checkbox"
                checked={completed}
                onChange={handleCompleted}
            />
            <div className="todo-row-infos">
                <p className="todo-item-title">{todoItem.title}</p>
                <p className="todo-item-deadline">{format(todoItem.deadlineDate, "dd/MM/yyyy")}</p>
            </div>
            <img
                className="delete-button"
                src={remove}
                alt="Delete"
                onClick={handleDelete}
            />
          
        </div>  
    );
};
