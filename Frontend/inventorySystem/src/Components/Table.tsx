const Table = () => {
  return (
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Nome</th>
          <th>Saldo</th>
          <th>
            <button>Cadastrar produto</button>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>1</td>
          <td>João</td>
          <td>R$ 1500,00</td>
          <td>-</td>
        </tr>
        <tr>
          <td>2</td>
          <td>Maria</td>
          <td>R$ 2300,50</td>
          <td>-</td>
        </tr>
        <tr>
          <td>3</td>
          <td>Carlos</td>
          <td>R$ 985,75</td>
          <td>-</td>
        </tr>
      </tbody>
    </table>
  );
};

export default Table;
