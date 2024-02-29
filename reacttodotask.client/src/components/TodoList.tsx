import React, { useState, useEffect } from 'react';
import logo from '../assets/done_outline-black.svg';
import add from '../assets/add-black-36dp.svg';
import './styles.css';
import { TodoItem } from '../models/TodoItem';
import { todoApiFactory } from '../services/todoApi';
import { TodoItemComponent } from './TodoItem';

export const TodoList: React.FC = () => {
    const [todos, setTodos] = useState<TodoItem[]>([]);
    const [todoInput, setTodoInput] = useState('');
    console.log(todoInput);

    useEffect(() => {
        handleGetTodos();
    }, []);

    const handleGetTodos = async () => {
        const { getTodos } = todoApiFactory();
        getTodos().then((result) => {
            console.log(result);
            if (result.status !== 'success') {
                console.log("getTodos" + result.status);
                //history.push('/login');
                return;
            }
            setTodos(result.data!);
        });
    }

    const handleCreateTodo = async () => {

        if (todoInput.length <= 10) {
            console.log("ssss")
        } else {

            const { createTodo, getTodos } = todoApiFactory();
            const todo: TodoItem = {
                id: 0,
                title: todoInput,
                completed: false,
            };
            setTodoInput('');

            const response = await createTodo(todo);

            if (response.status !== 'success') return;
            
            handleGetTodos();
            //const newTodo = response.data!;
            //setTodos([...todos, newTodo]);
        }
    };

    const handleDeleteTodo = async (todoItem: TodoItem) => {
        const { deleteTodo, getTodos } = todoApiFactory();
        const response = await deleteTodo(todoItem.id);
        

        if (response.status !== 'success') return;
        //setTodos(todos.filter((todo) => todo.id !== todoItem.id));
       
        handleGetTodos();
    };

    const handleCompleteTodo = async (todoItem: TodoItem) => {
        const { updateTodo, getTodos } = todoApiFactory();
        const response = await updateTodo(todoItem);

        if (response.status !== 'success') { 
            console.log("handleCompleteTodo : " + response.status);
            //history.push('/login');
        }

        handleGetTodos();
    };

    return (
        <div>
            <div className="new-todo-input">
                <input
                    type="text"
                    placeholder="new todo..."
                    onChange={(e) => setTodoInput(e.target.value)}
                    value={todoInput}
                />
                <button onClick={() => handleCreateTodo()}>
                    <img src={add} alt="Add" />
                </button>
            </div>
            <header className="todolist-title-container">
                <img src={logo} alt="logo" /> <strong>Todo List</strong>
            </header>
            <main className="todo-list-main">
                <ul>
                    {todos.map((todo) => (
                        <li key={todo.id}>
                            <TodoItemComponent
                                todoItem={todo}
                                onDelete={handleDeleteTodo}
                                onMarkAsCompleted={handleCompleteTodo}
                            />
                        </li>
                    ))}
                </ul>
            </main>
            
        </div>
    );
};
