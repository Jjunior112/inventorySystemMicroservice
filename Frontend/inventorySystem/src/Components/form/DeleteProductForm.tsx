const DeleteProductForm = () => {
  return (
    <>
      <h1>Excluir Produto</h1>

      <form id="productForm">
        <label for="productName">Nome do Produto:</label>
        <input
          type="text"
          id="productName"
          name="productName"
          placeholder="Digite o nome do produto"
          readonly
        />

        <label for="productCategory">Categoria:</label>
        <input
          type="text"
          id="productCategory"
          name="productCategory"
          placeholder="Digite a categoria do produto"
          readonly
        />

        <button type="submit" id="deleteProduct">
          Excluir Produto
        </button>
      </form>
    </>
  );
};

export default DeleteProductForm;
