import {
  trigger,
  animate,
  transition,
  style,
  group
 } from '@angular/animations'

const animations =  [
  trigger('pendingOrders', [
    transition('*=>void', [
      group([
        animate(1500, style({
          backgroundColor: 'green'
        })),
        animate(1500, style({
          transform: 'translateX(100px)',
          opacity: 0
        }))
      ])
    ])
  ])
]

export { animations }
