const AddProductForm = () => {
  return (
    <>
      <h1>Cadastrar Produto</h1>
      <form id="productForm">
        <label for="productName">Nome do Produto:</label>
        <input
          type="text"
          id="productName"
          name="productName"
          placeholder="Digite o nome do produto"
          required
        />

        <label for="productCategory">Categoria:</label>
        <input
          type="text"
          id="productCategory"
          name="productCategory"
          placeholder="Digite a categoria do produto"
          required
        />

        <button type="submit">Criar Produto</button>
      </form>
    </>
  );
};

export default AddProductForm;
