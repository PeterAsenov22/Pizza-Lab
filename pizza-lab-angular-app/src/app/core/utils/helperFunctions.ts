export function getTotalSum(products) {
  let total = 0
  for (const pr of products) {
    total += pr.price * pr.quantity
  }

  return total
}

export function toLocaleString(date) {
  return new Date(date).toLocaleString()
}
