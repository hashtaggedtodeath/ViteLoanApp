<template>
  <header>
    <h2>Loan Management App</h2>
    <nav>
      <RouterLink to="/">Список заявок</RouterLink>
      <RouterLink to="/create">Создать заявку</RouterLink>
    </nav>
  </header>

  <main>
    <RouterView />
  </main>
</template>

<style scoped>
header {
  background-color: #2c3e50;
  color: white;
  padding: 15px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

nav a {
  color: white;
  margin: 0 10px;
  text-decoration: none;
}

nav a.router-link-active {
  text-decoration: underline;
}
</style>
