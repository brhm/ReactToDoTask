import { TodoList } from '../src/components/TodoList';
import './App.css';


function App() {
    
    console.log("App")
   
    return (
        <div>
            <h1 id="tabelLabel">TODO LIST</h1>
            <p>This application is used the React, Typescript and .Net Core Web Api</p>
            <TodoList />
        </div>
    );

}

export default App;